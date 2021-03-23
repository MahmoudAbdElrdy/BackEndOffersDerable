using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO;
using BackEnd.Service.DTO.Client;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationClientController : ControllerBase
    {
        #region privateFild
        private IServicesNotificationCustomer ServicesNotificationClient;
        #endregion

        #region NotificationClientController(IServicesNotificationClient _ServicesNotificationClient)
        public NotificationClientController(IServicesNotificationCustomer _ServicesNotificationClient)
        {
            ServicesNotificationClient = _ServicesNotificationClient;
        }
        #endregion

        #region Get : api/NotificationClient/GetAll
        [HttpGet("GetAll")]
        public IResponseDTO GetAll()
        {
            var result = ServicesNotificationClient.GetAll();
            return result;
        }
        #endregion

      

       

        #region Get : api/ConstructionLicense/GetByClient
        [HttpGet("GetByClient")]
        public IResponseDTO GetByClient(string ApplicationUserId)
        {
            var result = ServicesNotificationClient.GetByClient(ApplicationUserId);
            return result;
        }
        #endregion

        

        #region Delete : api/NotificationClient/Delete
        [HttpDelete("DeleteNotification")]
        public IResponseDTO DeleteNotification(NotificationDto model)
        {
            var result = ServicesNotificationClient.DeleteNotification(model);
            return result;
        }
        #endregion

        #region Put : api/NotificationClient/Remove
        [HttpPut("Remove")]
        public IResponseDTO Remove(int NotificationCid)
        {
            var result = ServicesNotificationClient.Remove(NotificationCid);
            return result;
        }
        #endregion

        #region Post : api/NotificationClient/SaveNew
        
        [HttpPost("SaveNewAdmin")]
        public async Task<IResponseDTO> SaveNew([FromBody] NotificationDto model)
        {
            var result = await ServicesNotificationClient.InsertAsync(model);
            return result;
        }
        #endregion
    }
}
