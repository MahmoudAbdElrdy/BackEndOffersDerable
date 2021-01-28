using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.ISercice;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Service.Service
{
    public class IdentityServices : BaseServices, IidentityServices
    {
        private readonly Random _random = new Random();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationSettings _jwtSettings;
        private readonly TokenValidationParameters _TokenValidationParameters;
        private readonly BakEndContext _dataContext;
        private readonly IemailService _emailService;


        public IdentityServices(UserManager<ApplicationUser> userManager, IResponseDTO response,
      ApplicationSettings jwtSettings,
      TokenValidationParameters TokenValidationParameters,
      RoleManager<IdentityRole> roleManager,
      IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper,
      BakEndContext dataContext,
      IemailService emailService)
            : base(unitOfWork, responseDTO, mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
            _TokenValidationParameters = TokenValidationParameters;
            _dataContext = dataContext;
            _emailService = emailService;
            _response = response;

        }

        public async Task<AuthenticationResult> LoginAsync(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                user = _unitOfWork.ApplicationUser.GetEntity(x=>x.PhoneNumber==Email);
              //  user = await _userManager.FindByNameAsync(Email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Message = "User does not Exist"
                };
            }


            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Message = "Password  wrong"
                };
            }
            if (user.confirmed == null || user.confirmed == false)
            {
                return new AuthenticationResult
                {
                    Message = "User Must Send Verfication Code"
                };
            }
            return await GenerateAutheticationForResultForUser(user);
        }


        private ClaimsPrincipal GetPrincipalFromToken(string Token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(Token, _TokenValidationParameters, out var validtionToken);
                if (!IsJwtWithValidationSecurityAlgorithm(validtionToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidationSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
              jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<IResponseDTO> RegisterAsync(string  Role, string fullName, string UserName, string Email, string Password, string Image, string PhoneNumber)
        {
            var existingUser = await _userManager.FindByEmailAsync(Email);
            var UserId = "";
            if (existingUser != null)
            {
                return new ResponseDTO
                {
                    Data = null,
                    Code = 400,
                    Message = "User with this email adress already Exist"
                };
            }
            int num = _random.Next();
            var newUser = new ApplicationUser
            {
                Email = Email,
                UserName = UserName,
                FullName = fullName,
                verficationCode = num,
                PhoneNumber = PhoneNumber,
                confirmed = true,
             
            };



            //-----------------------------add Role to token------------------
         
           
            if (!string.IsNullOrEmpty(Role))
            {
                var createdUser = await _userManager.CreateAsync(newUser, Password);
                UserId = newUser.Id;
                if (!createdUser.Succeeded)
                {
                    return new ResponseDTO
                    {
                        Data = null,
                        Code = 400,
                        Message = createdUser.Errors.Select(x => x.Description).FirstOrDefault()
                    };

                }
                await _userManager.AddToRoleAsync(newUser,Role);
            }
            //-----------------------------------------------------------------
            //var res = await sendVerficationToEMail(newUser.Email);
            //if (res != true)
            //{
            //    return new ResponseDTO
            //    {
            //        Data = null,
            //        Code = 400,
            //        Message = createdUser.Errors.Select(x => "email not send").FirstOrDefault(),
            //    };

            //}
            //else
            //{
            return new ResponseDTO
            {
                Data = UserId,
                Code = 200,
                Message = "OK"
            };

            // }




        }

        //return await GenerateAutheticationForResultForUser(newUser);
        private async Task<AuthenticationResult> GenerateAutheticationForResultForUser(ApplicationUser user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.JWT_Secret);
            var claims = new List<Claim> {
          new Claim(JwtRegisteredClaimNames.Sub,user.Email),
          new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Email,user.Email),
          new Claim("id",user.Id)
          };

            //get claims of user---------------------------------------
            var Userclaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(Userclaims);
            //------------------------Add Roles to claims-----------------------------------
            var userRols = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRols)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            //---------------------------------------------------------
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(TokenDescriptor);
            await _dataContext.SaveChangesAsync();
            return new AuthenticationResult
            {
                Success = true,
                Token = TokenHandler.WriteToken(token)

            };
        }
        public async Task<Boolean> sendVerficationToEMail(string Email)
        {
            int num = _random.Next();
            return await _emailService.sendVerfication(num, Email);
        }
        

        public async Task<Result> verfayUser(UserVerfayRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user.verficationCode == request.verficationCode)
            {
                user.confirmed = true;
                await _userManager.UpdateAsync(user);
                return new Result { success = true, data = user, code = 200, message = "ok" };
            }
            else
            {
                return new Result { success = false, data = user, code = 400, message = "Falied" };
            }

        }

        public IResponseDTO GetRoles()
        {
            var Roles = _dataContext.Roles;
            try
            {
                return new ResponseDTO
                {
                    Data = Roles,
                    Code = 200,
                    Message = "OK"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Data = Roles,
                    Code = 200,
                    Message = "OK"
                };
            }
           

        
        }
    }
}
