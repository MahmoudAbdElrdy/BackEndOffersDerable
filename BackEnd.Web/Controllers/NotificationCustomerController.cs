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

        #region Get : api/NotificationClient/GetAvailable
        [HttpGet("GetAvailable")]
        public IResponseDTO GetAvailable()
        {
            var result = ServicesNotificationClient.GetAvailable();
            return result;
        }
        #endregion

        #region Get : api/NotificationClient/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesNotificationClient.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Get : api/ConstructionLicense/GetByClient
        [HttpGet("GetByClient")]
        public IResponseDTO GetByClient(int ClientId)
        {
            var result = ServicesNotificationClient.GetByClient(ClientId);
            return result;
        }
        #endregion

        #region Put : api/NotificationClient/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]NotificationClientDto model)
        {

            var result = ServicesNotificationClient.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/NotificationClient/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesNotificationClient.Delete(id);
            return result;
        }
        #endregion

        #region Put : api/NotificationClient/Remove
        [HttpPut("Remove")]
        public IResponseDTO Remove([FromBody] NotificationClientDto model)
        {
            var result = ServicesNotificationClient.Remove(model);
            return result;
        }
        #endregion

        #region Post : api/NotificationClient/SaveNew
        [HttpPost("SaveNew")]
        public async Task<IResponseDTO> SaveNew([FromBody] NotificationClientDto model)
        {
            var result = await ServicesNotificationClient.InsertAsync(model);
            return result;
        }
        [HttpPost("SaveNewAdmin")]
        public async Task<IResponseDTO> SaveNew([FromBody] NotificationDto model)
        {
            var result = await ServicesNotificationClient.InsertAsync(model);
            return result;
        }
        #endregion
    }
}
