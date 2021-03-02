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
        public ActionResult<IResponseDTO> GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesDiscount.GetAll(pageNumber, pageSize);
             if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion
        #region Get : api/Discount/GetAll
        [HttpGet("GetAllProdcut")]
        public ActionResult<IResponseDTO> GetAllProdcut(int pageNumber = 0, int pageSize = 0, int CategoryId = 0, int CityId = 0,string ProdcutName="", string ApplicationUserId = "")
        {
            var result = ServicesDiscount.GetAllProdcut(pageNumber, pageSize, CategoryId,CityId,ProdcutName, ApplicationUserId);
             if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion
        #region Get : api/Discount/GetById
        [HttpGet("GetById")]
        public ActionResult<IResponseDTO> GetById(int id)
        {
            var result = ServicesDiscount.GetByIdAsync(id);
             if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion
        #region Get : api/Discount/GetById
        [HttpGet("GetProdcutById")]
        public ActionResult<IResponseDTO> GetProdcutById(int id)
        {
            var result = ServicesDiscount.GetProdcutById(id);
             if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        [HttpGet("GetDiscountByProduct")]
        public ActionResult<IResponseDTO> GetDiscountByProduct(int id)
        {
            var result = ServicesDiscount.GetDiscountByProduct(id);
             if (result.Code == 404)
            {  return NotFound(result);} 
            return Ok(result);
        }
        #endregion
        #region Put : api/Discount/Update
        [HttpPost("Update")]
        public ActionResult<IResponseDTO> Update([FromBody]DiscountDto model)
        {

            var result = ServicesDiscount.Update(model);
             if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion

        #region Delete : api/Discount/Delete
        [HttpGet("Delete")]
        public ActionResult<IResponseDTO> Delete(int id)
        {
            var result = ServicesDiscount.Delete(id);
             if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion

       
        #region Post : api/Discount/SaveNew
        [HttpPost("SaveNew")]
        public ActionResult<IResponseDTO> SaveNew([FromBody] DiscountDto model)
        {

            if (model.Id == 0 )
            {
                var result = ServicesDiscount.Insert(model);
                if (result.Code == 404) { return NotFound(result); }
                return Ok(result);

            }
            else
            {
                var result = ServicesDiscount.Update(model);
                if (result.Code == 404) { return NotFound(result); }
                return Ok(result);

            }
        }
        #endregion
    }
}
