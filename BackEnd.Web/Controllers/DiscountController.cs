using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO.Prodcuts;
using BackEnd.Service.IService;
using BackEnd.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        #region privateFild
        private IServiceDiscount ServicesDiscount;
        #endregion

        #region DiscountController(IServicesDiscount _ServicesDiscount)
        public DiscountController(IServiceDiscount _ServicesDiscount)
        {
            ServicesDiscount = _ServicesDiscount;
        }
        #endregion

        #region Get : api/Discount/GetAll
        [HttpGet("GetPage")]
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesDiscount.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
      
        #region Get : api/Discount/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesDiscount.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/Discount/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]DiscountDto model)
        {

            var result = ServicesDiscount.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/Discount/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesDiscount.Delete(id);
            return result;
        }
        #endregion

       
        #region Post : api/Discount/SaveNew
        [HttpPost("SaveNew")]
        public IResponseDTO SaveNew([FromBody] DiscountDto model)
        {
            var result =  ServicesDiscount.Insert(model);
            return result;
        }
        #endregion
    }
}
