using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.Service;
using BackEnd.Service.ISercice;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.V1
{
 
  public class IdentityController: Controller
  {
        private IidentityServices _identityService;
        private readonly BakEndContext _BakEndContext;
        private readonly IResponseDTO _response;
        private readonly Random _random = new Random();
        public IdentityController(IidentityServices identityServices, IResponseDTO responseDTO,
      BakEndContext Context)
        {
            _identityService = identityServices;
            _BakEndContext = Context;
            _response = responseDTO;
        }
        [HttpPost("api/Register")]
        public async Task<ActionResult<IResponseDTO>> Register([FromBody] UserRegisteration request)
        {

            var authResponse = await _identityService.RegisterAsync(request.Role,request.FullName, request.UserName, request.Email, request.Password,request.Image,request.PhoneNumber);
            authResponse.Data = authResponse.Data;
            if(authResponse.Code==404)
            {
                return NotFound(authResponse);
            }
            return Ok(authResponse);
        }
        [HttpPost("api/GetRoles")]
        public ActionResult<ResponseDTO> GetRoles()
        {

            var authResponse =  _identityService.GetRoles();

            return Ok(authResponse);
        }
        [HttpPost("api/Login")]
        public async Task<ActionResult<IResponseDTO>> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (authResponse.Code == 200)
            {

                
                return Ok(authResponse);
            }
            else
            {
              
                return NotFound(authResponse);
            }

          
        }
        [HttpPost("ForegetPassword")]
        public async Task<ActionResult<IResponseDTO>> ForegetPassword([FromBody] ForgetPasswordEmail request)
        {

            int num = _random.Next();
            var res = await _identityService.updateresetPasswordCode(num, request.Email);
           
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok( res);
        }


        [HttpPost("resetPasswordVerfayCode")]
        public async Task<ActionResult<IResponseDTO>> resetPasswordVerfayCode([FromBody] UserVerfayResetPasswordCode request)
        {
            var res = await _identityService.resetPasswordVerfayCode(request);
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
       

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<IResponseDTO>> ResetPassword(ResetPasswordVm resetpasswordVm)
        {
            var res = await _identityService.ResetPassword(resetpasswordVm);
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok();
        }
        [HttpPost("ResetPasswordMobile")]
        public ActionResult<IResponseDTO> ResetPassword(ResetPasswordMobile resetpasswordVm)
        {
            var res = _identityService.RestPasswordByPhone(resetpasswordVm);
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpPost("checkPhone")]
        public ActionResult<IResponseDTO> checkPhone(string Phone)
        {
            var res = _identityService.checkPhone(Phone);
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpPost("verifyAccount")]
        public ActionResult<IResponseDTO> verifyAccount(string Phone)
        {
            var res = _identityService.verifyAccount(Phone);
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpGet("GetProfile")]
        public ActionResult<IResponseDTO> GetProfile(string UserId)
        {
            // string UserId2  = HttpContext.User.Claims.Single(x => x.Type == "id").Value;
            var res = _identityService.GetProfileClient(UserId);
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpPost("UpdateUser")]
        public ActionResult<IResponseDTO> UpdateUser([FromBody] UpdateUser User)
        {
            var res = _identityService.UpdateUser(User);
            if (res.Code == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpPost("UploadImage")]
        public ActionResult<IResponseDTO> Upload(string ApplicationUserId)
        {
            IResponseDTO res=new ResponseDTO();
            try
            {
                var name = BackEnd.Web.Helper.UploadHelper.SaveFile(Request.Form.Files[0], "File");
                //string path = xx[0];
                if (!string.IsNullOrEmpty(name)&& !string.IsNullOrEmpty(ApplicationUserId))
                {
                     res = _identityService.UpdateImage(ApplicationUserId, name);
                    if (res.Code == 404)
                    {
                        return NotFound(res);
                    }
                    return Ok(res);
                }
                else
                {
                    res = new ResponseDTO()
                    {
                        Code = 404,
                        Message = "Empty Image" ,
                        Data = null,
                    };
                    return NotFound(res);
                }
            }
            catch (Exception ex)
            {
                res = new ResponseDTO()
                {
                    Code = 404,
                    Message = "Error " + ex.Message,
                    Data = null,
                };
                return NotFound(res);
            }
           // return Ok(res);
        }
    }
}
