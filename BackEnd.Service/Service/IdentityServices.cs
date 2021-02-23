using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.Client;
using BackEnd.Service.DTO.Companies;
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

        public async Task<IResponseDTO> LoginAsync(string Email, string Password)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                user = _unitOfWork.ApplicationUser.GetEntity(x=>x.PhoneNumber==Email);
            if (user == null)
                user = _unitOfWork.ApplicationUser.GetEntity(x => x.UserName == Email);
            //  user = await _userManager.FindByNameAsync(Email);
            if (user == null)
            {
                return new ResponseDTO
                {
                    Message = "User does not Exist"
                };
            }


            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!userHasValidPassword)
            {
                return new ResponseDTO
                {
                    Message = "Password  wrong"
                };
            }
            if (user.confirmed == null || user.confirmed == false)
            {
                return new ResponseDTO
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
                    Code = 404,
                    Message = "User with this email adress already Exist"
                };
            }
            var existingUser2 = _dataContext.Users.Where(x => x.PhoneNumber == PhoneNumber);
            if (existingUser2.Count() > 0)
            {
                return new ResponseDTO
                {
                    Data = null,
                    Code = 404,
                    Message  = "User with this PhoneNumber adress already Exist"
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
                Image=Image
             
            };



            //-----------------------------add Role to token------------------
         
           
            if (!string.IsNullOrEmpty(Role)&&(Role== "Admin"||Role== "Client"||Role== "Company"))
            {
                var createdUser = await _userManager.CreateAsync(newUser, Password);
                UserId = newUser.Id;
                if(Role== "Client")
                {
                    Client client = new Client();
                    client.ApplicationUserId = newUser.Id;
                    _unitOfWork.Client.Insert(client);
                    _unitOfWork.Save();
                    
                }
                if (!createdUser.Succeeded)
                {
                    return new ResponseDTO
                    {
                        Data = null,
                        Code = 404,
                        Message = createdUser.Errors.Select(x => x.Description).FirstOrDefault()
                    };

                }
                

                await _userManager.AddToRoleAsync(newUser,Role);
                var Respones = SendMessage(PhoneNumber, num.ToString());
                return new ResponseDTO
                {
                    Data = UserId,
                    Code = 200,
                    Message = "OK"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    Data = UserId,
                    Code = 404,
                    Message = "Role Not Found"
                };
            }
            //-----------------------------------------------------------------
            //var res = await sendVerficationToEMail(newUser.Email);
            //if (res != true)
            //{
            //    return new ResponseDTO
            //    {
            //        Data = null,
            //        Code = 404,
            //        Message = createdUser.Errors.Select(x => "email not send").FirstOrDefault(),
            //    };

            //}
            //else
            //{
           

            // }




        }
        public IResponseDTO SendMessage(string PhoneNumber, string Code)
        {
            string myURI = "https://api.bulksms.com/v1/messages";

            // change these values to match your own account
            string myUsername = "dealsapp2";
            string myPassword = "DealsApp123";

            // the details of the message we want to send
            // String strPost = "?user=9030888111&password=password&msg=" + message + "&sender=OPTINS" + "&mobile=mobnum" + "&type=1";
            string myData = "{to: " + AddDoubleQuotes(PhoneNumber) + ", body:" + AddDoubleCode(Code) + "}";
            //string myData = "" +
            //    "{to: \"+'PhoneNumber'+\"," +
            //    " body:\"تم تاكيد شراء العرض  توجه الي التاجر لاستلام العرض  ومعك كود العرض:+'Code'+ \"}";
            //          
            var request = WebRequest.Create(myURI);

            // supply the credentials
            request.Credentials = new NetworkCredential(myUsername, myPassword);
            request.PreAuthenticate = true;
            // we want to use HTTP POST
            request.Method = "POST";
            // for this API, the type must always be JSON
            request.ContentType = "application/json";

            // Here we use Unicode encoding, but ASCIIEncoding would also work
            var encoding = new UnicodeEncoding();
            var encodedData = encoding.GetBytes(myData);

            // Write the data to the request stream
            var stream = request.GetRequestStream();
            stream.Write(encodedData, 0, encodedData.Length);
            stream.Close();

            // try ... catch to handle errors nicely
            try
            {
                // make the call to the API
                var response = request.GetResponse();

                // read the response and print it to the console
                var reader = new StreamReader(response.GetResponseStream());
                Console.WriteLine(reader.ReadToEnd());
                return new ResponseDTO()
                {
                    Data = reader.ReadToEnd(),
                    Code = 200,
                    Message = "Sent Successfully"
                };
            }
            catch (WebException ex)
            {
                // show the general message
                Console.WriteLine("An error occurred:" + ex.Message);

                // print the detail that comes with the error
                var reader = new StreamReader(ex.Response.GetResponseStream());
                Console.WriteLine("Error details:" + reader.ReadToEnd());

                // dynamic stuff1 = Newtonsoft.Json.JsonConvert.DeserializeObject(res);

                _response.Data = reader.ReadToEnd();
                _response.Message = ex.Message;
                _response.Code = 404;
                return _response;
            }

        }
        public string AddDoubleQuotes(string value)
        {
            return "\"" + value + "\"";
        }
        public string AddDoubleCode(string value)
        {
            return "\" كود التسجيل:" + value + "\"";
        }
        //return await GenerateAutheticationForResultForUser(newUser);
        private async Task<ResponseDTO> GenerateAutheticationForResultForUser(ApplicationUser user)
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
            dynamic User = new ExpandoObject();
            User.token = TokenHandler.WriteToken(token);
            User.Id = user.Id;
            return new ResponseDTO
            {
                Code=200,
                Data = User

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
                return new Result { success = false, data = user, code = 404, message = "Falied" };
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
        public async Task<IResponseDTO> GetAllCompanyAsync()
        { 
            try
            {
                var users = await _userManager.GetUsersInRoleAsync("Company");


                var usersDto = new List<ExpandoObject>();
                foreach (var Model in users)
                {
                    dynamic User = new ExpandoObject();
                    User.Id = Model.Id;
                    User.UserName = Model.UserName;
                    User.FullName = Model.FullName;
                    usersDto.Add(User);
                }

                _response.Data = usersDto;
                _response.Code = 200;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Code = 200;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public IResponseDTO checkPhone(string PhoneNumber)
        {
            try
            {
                var Check = _unitOfWork.ApplicationUser.GetEntity(x => x.PhoneNumber == PhoneNumber);
                int num = _random.Next();
              
                if (Check != null)
                {
                    var Respones = SendMessage(PhoneNumber, num.ToString());
                    if (Respones.Code == 200)
                    {
                         Check.resetPasswordCode = num;
                        _unitOfWork.ApplicationUser.Update(Check);
                       
                        var Save = _unitOfWork.Save();
                        if (Save == "200")
                        {
                            _response.Data = true;
                            _response.Code = 200;
                            _response.Message = "OK";
                        }
                        else
                        {
                            _response.Data = false;

                            _response.Code = 404;
                            _response.Message = "Not Saved";
                        }

                    }
                    else
                    {
                        _response.Data = false;

                        _response.Code = 404;
                        _response.Message = "SMS failure";
                    }
                   
                }
                else
                {
                    _response.Data = false;

                    _response.Code = 404;
                    _response.Message = PhoneNumber + " Not Found";
                }

            }
            catch (Exception ex)
            {
                _response.Data = null;

                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }
        public IResponseDTO verifyAccount(string PhoneNumber,string resetPasswordCode)
        {
            try
            {
                var Check = _unitOfWork.ApplicationUser.GetEntity(x => x.PhoneNumber == PhoneNumber&&x.resetPasswordCode==int.Parse(resetPasswordCode));
                if (Check != null)
                {
                    Check.confirmedMobile = true;
                    Check.confirmed = true;
                   _response.Data = Check.PhoneNumber;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;

                    _response.Code = 404;
                    _response.Message = PhoneNumber + " Not Found";
                }

            }
            catch (Exception ex)
            {
                _response.Data = null;

                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public IResponseDTO RestPasswordByPhone(ResetPasswordMobile resetpasswordVm)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = _unitOfWork.ApplicationUser.GetEntity(x => x.PhoneNumber == resetpasswordVm.PhoneNmber);
            if (!string.IsNullOrEmpty(resetpasswordVm.Password))
                user.PasswordHash = passwordHasher.HashPassword(user, resetpasswordVm.Password);
            _unitOfWork.ApplicationUser.Update(user);
            var Save = _unitOfWork.Save();
            if (Save == "200")
            {
                return new ResponseDTO
                {

                    Code = 200,
                    Message = "reset password success",
                    Data = user.PhoneNumber
                };
            }
            else
            {
                return new ResponseDTO
                {

                    Code = 403,
                    Message = "reset password faild",
                    Data = user.PhoneNumber
                };
            }
        }
        public IResponseDTO GetProfileClient(string ApplicationUserId) 
        {
            try
            {
                      dynamic UserProfile = new ExpandoObject();
               
                    var user2 = _unitOfWork.ApplicationUser.GetEntity(x => x.Id == ApplicationUserId);
                    

                    UserProfile.ApplicationuserId = ApplicationUserId;
                    UserProfile.Image = user2.Image;
                    UserProfile.Email = user2?.Email;
                    UserProfile.FullName = user2?.FullName;
                    UserProfile.userName = user2?.UserName;
                    UserProfile.PhoneNumber = user2?.PhoneNumber;
                    UserProfile.PhoneNumberConfirmed = user2?.PhoneNumberConfirmed;
                    UserProfile.EmailConfirmed = user2?.EmailConfirmed;
                    UserProfile.confirmedMobile = user2 ?.confirmedMobile;
                    UserProfile.confirmed = user2?.confirmed;

                _response.Code = 200;
                _response.Data = UserProfile;
                _response.Message = "OK";
                // UserProfile.Id = user.Id;
            }
            catch (Exception ex)
            {
                _response.Code = 200;
                _response.Data = null;
                _response.Data = ex.Message;

            }

            return _response;
        }
        public IResponseDTO UpdateUser(UpdateUser user)
        {
            try
            {
                var User = _unitOfWork.ApplicationUser.GetByID(user.ApplicationUserId);
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                             if (!string.IsNullOrEmpty(user.Password))
                    User.PasswordHash = passwordHasher.HashPassword(User, user.Password);

                User.FullName = user.FullName;
                User.UserName = user.UserName;
                User.PhoneNumber = user.PhoneNumber;
                User.Email = user.Email;
               

                _unitOfWork.ApplicationUser.Update(User);
               

                var Result = _unitOfWork.Save();
                if (Result == "200")
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "OK";

                }
                else
                {
                    _response.Data = null;
                    _response.Code = 404;
                    _response.Message = Result;
                }

            }
            catch (Exception ex)
            {
                _response.Data = ex.Message;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public IResponseDTO UpdateImage(string ApplicationUserId, string Image)
        { 
            try
            {
                var User = _unitOfWork.ApplicationUser.GetByID(ApplicationUserId);
              
                User.Image = Image;
              


                _unitOfWork.ApplicationUser.Update(User);


                var Result = _unitOfWork.Save();
                if (Result == "200")
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "OK";

                }
                else
                {
                    _response.Data = null;
                    _response.Code = 404;
                    _response.Message = Result;
                }

            }
            catch (Exception ex)
            {
                _response.Data = ex.Message;
                _response.Code = 404;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public IResponseDTO GetProfile(string ApplicationUserId)
        {
            try
            {
                var user = _unitOfWork.Company.Get(x => x.ApplicationUserId == ApplicationUserId, includeProperties: "ApplicationUser").FirstOrDefault();
                dynamic UserProfile = new ExpandoObject();
                if (user != null)
                {
                    UserProfile.Id = user.Id;
                    UserProfile.ApplicationUserId = user.ApplicationUserId;
                    UserProfile.Latitude = user.Latitude;
                    UserProfile.Longitude = user.Longitude;
                    UserProfile.Email = user.User?.Email;
                    UserProfile.FullName = user.User?.FullName;
                    UserProfile.UserName = user.User?.UserName;
                    UserProfile.PhoneNumber = user.User?.PhoneNumber;
                    UserProfile.PhoneNumberConfirmed = user.User?.PhoneNumberConfirmed;
                    UserProfile.EmailConfirmed = user.User?.EmailConfirmed;
                    UserProfile.confirmedMobile = user.User?.confirmedMobile;
                    UserProfile.confirmed = user.User?.confirmed;

                }
                if (user == null)
                {
                    var user2 = _unitOfWork.ApplicationUser.GetEntity(x => x.Id == ApplicationUserId);
                    UserProfile.Id = null;

                    UserProfile.ApplicationuserId = null;
                  
                    UserProfile.Latitude = null;
                    UserProfile.Longitude = null;
                    UserProfile.Email = user2?.Email;
                    UserProfile.FullName = user2?.FullName;
                    UserProfile.userName = user2?.UserName;
                    UserProfile.PhoneNumber = user2?.PhoneNumber;
                    UserProfile.PhoneNumberConfirmed = user2?.PhoneNumberConfirmed;
                    UserProfile.EmailConfirmed = user2?.EmailConfirmed;

                }
                _response.Code = 200;
                _response.Data = UserProfile;
                _response.Message = "OK";
                // UserProfile.Id = user.Id;
            }
            catch (Exception ex)
            {
                _response.Code = 200;
                _response.Data = null;
                _response.Data = ex.Message;

            }

            return _response;
        }
        public async Task<IResponseDTO> updateresetPasswordCode(int num, string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            User.resetPasswordCode = num;
            await _userManager.UpdateAsync(User);
            return new ResponseDTO
            {
                Code = 200,
                Message = "code sent successfuly",

                Data = null
            };
        }

        public async Task<Boolean> sendCodeResetPasswordToEMail(int restCode, string Email)
        {
            return await _emailService.sendResetPasswordCode(restCode, Email);
        }


        public async Task<IResponseDTO> resetPasswordVerfayCode(UserVerfayResetPasswordCode request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user.resetPasswordCode == request.resetCode)
            {
                return new ResponseDTO { Data = null, Code = 200, Message = "confirmed scuccess" };
            }
            else
            {
                return new ResponseDTO { Code = 403, Message = "confirmed faild", Data = null };
            }

        }

        public async Task<IResponseDTO> ResetPassword(ResetPasswordVm resetpasswordVm)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = await _userManager.FindByEmailAsync(resetpasswordVm.Email);
            if (!string.IsNullOrEmpty(resetpasswordVm.Password))
                user.PasswordHash = passwordHasher.HashPassword(user, resetpasswordVm.Password);
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResponseDTO
                {

                    Code = 200,
                    Message = "reset password success",
                    Data = result
                };
            }
            else
            {
                return new ResponseDTO
                {

                    Code = 403,
                    Message = "reset password faild",
                    Data = result
                };
            }
        }
      

    }
}
