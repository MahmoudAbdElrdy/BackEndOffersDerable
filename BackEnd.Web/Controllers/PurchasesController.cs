using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO.Client;
using BackEnd.Service.DTO.Prodcuts;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase 
    {
        #region privateFild
        private IServicesPurchases ServicesPurchases;
        private readonly Random _random = new Random();
        #endregion

        #region PurchasesController(IServicesPurchases _ServicesPurchases)
        public PurchasesController(IServicesPurchases _ServicesPurchases)
        {
            ServicesPurchases = _ServicesPurchases;
        }
        #endregion

        #region Get : api/Purchases/GetAll
        [HttpGet("GetPurchasesByUserId")]
        public ActionResult<IResponseDTO> GetPurchasesByUserId(int pageNumber = 0, int pageSize = 0, string ApplicationUserId = "")
        {
            var result = ServicesPurchases.GetPurchasesByUserId(pageNumber, pageSize, ApplicationUserId);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
          
        }
        #endregion
        #region Get : api/Purchases/GetAllClient
        [HttpGet("GetAllClient")]
        public ActionResult<IResponseDTO> GetAllClient(int pageNumber = 0, int pageSize = 0)
        {
            var result = ServicesPurchases.GetAllClient(pageNumber, pageSize);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
          
        }
        #endregion
        [HttpGet("RandomCode")]
        public  ActionResult RandomCode() 
        {
            int num = _random.Next();
            return Ok(num);
        }
        //#region Get : api/Purchases/GetById
        //[HttpGet("GetById")]
        //public ActionResult<IResponseDTO> GetById(int id)
        //{
        //    var result = ServicesPurchases.GetByIdAsync(id);
        //    if (result.Code == 404)
        //    {
        //        return NotFound(result);
        //    }
        //    return Ok(result);
        //}
        //#endregion

        //#region Put : api/Purchases/Update
        //[HttpPost("Update")]
        //public ActionResult<IResponseDTO> Update([FromBody]PurchasesDto model)
        //{

        //    var result = ServicesPurchases.Update(model);
        //    if (result.Code == 404)
        //    {
        //        return NotFound(result);
        //    }
        //    return Ok(result);
        //}
        //#endregion

        //#region Delete : api/Purchases/Delete
        //[HttpGet("Delete")]
        //public ActionResult<IResponseDTO> Delete(int id)
        //{
        //    var result = ServicesPurchases.Delete(id);
        //    if (result.Code == 404)
        //    {
        //        return NotFound(result);
        //    }
        //    return Ok(result);
        //}
        //#endregion


        #region Post : api/Purchases/SaveNew
        [HttpPost("SaveNew")]
        public ActionResult<IResponseDTO> SaveNew([FromBody] PurchasesDto model)
        {
            var result =  ServicesPurchases.Insert(model);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion
    }
}
