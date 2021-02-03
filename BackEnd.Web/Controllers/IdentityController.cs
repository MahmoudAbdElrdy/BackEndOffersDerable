using BackEnd.BAL.ApiRoute;
using BackEnd.BAL.Models;
using BackEnd.DAL.Context;
using BackEnd.Service;
using BackEnd.Service.ISercice;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers.V1
{
 
  public class IdentityController: Controller
  {
        private IidentityServices _identityService;
        private readonly BakEndContext _BakEndContext;
        private readonly IResponseDTO _response;
        public IdentityController(IidentityServices identityServices, IResponseDTO responseDTO,
      BakEndContext Context)
        {
            _identityService = identityServices;
            _BakEndContext = Context;
            _response = responseDTO;
        }
        [HttpPost("api/Register")]
        public async Task<IResponseDTO> Register([FromBody] UserRegisteration request)
        {

            var authResponse = await _identityService.RegisterAsync(request.Role,request.FullName, request.UserName, request.Email, request.Password,request.Image,request.PhoneNumber);
            authResponse.Data = authResponse.Data;
            return authResponse;
        }
        [HttpPost("api/GetRoles")]
        public IResponseDTO GetRoles()
        {

            var authResponse =  _identityService.GetRoles();
          
            return authResponse;
        }
        [HttpPost("api/Login")]
        public async Task<IResponseDTO> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);
            if (authResponse.Code != 200)
            {

                _response.Data = authResponse;
                _response.Message = authResponse.Message;
                _response.Code = 200;
            }
            else
            {
                _response.Data = authResponse;
                _response.Message = authResponse.Message;
                _response.Code =400;
            }

            return _response;
        }

    }
}
