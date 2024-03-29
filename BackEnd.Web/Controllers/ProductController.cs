﻿using System;
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
    public class ProductController : ControllerBase
    {
        #region privateFild
        private IProdcutServices ServicesProduct;
        #endregion

        #region ProductController(IServicesProduct _ServicesProduct)
        public ProductController(IProdcutServices _ServicesProduct)
        {
            ServicesProduct = _ServicesProduct;
        }
        #endregion

        #region Get : api/Product/GetAll
        [HttpGet("GetPage")]
        public ActionResult<IResponseDTO> GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesProduct.GetAll(pageNumber, pageSize);
           if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        [HttpGet("GetAllArchive")]
        public ActionResult<IResponseDTO> GetAllArchive(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesProduct.GetAllArchive(pageNumber, pageSize);
           if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion
      
        #region Get : api/Product/GetById
        [HttpGet("GetById")]
        public ActionResult<IResponseDTO> GetById(int id)
        {
            var result = ServicesProduct.GetByIdAsync(id);
           if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion

        #region Put : api/Product/Update
        [HttpPost("Update")]
        public ActionResult<IResponseDTO> Update([FromBody]ProductDto model)
        {

            var result = ServicesProduct.Update(model);
           if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion

        #region Delete : api/Product/Delete
        [HttpGet("Delete")]
        public ActionResult<IResponseDTO> Delete(int id)
        {
            var result = ServicesProduct.Delete(id);
           if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        [HttpGet("RemoveFromDB")]
        public ActionResult<IResponseDTO> RemoveFromDB(int id)
        {
            var result = ServicesProduct.RemoveFromDB(id);
           if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion
        #region Post : api/Product/SaveNew
        [HttpPost("SaveNew")]
        public ActionResult<IResponseDTO> SaveNew([FromBody] ProductDto model)
        {
            var result =  ServicesProduct.Insert(model);
           if (result.Code == 404) {  return NotFound(result);}  return Ok(result);
        }
        #endregion

        #region Post : api/Product/SaveNew
        [HttpPost("Addfavourite")]
        public ActionResult<IResponseDTO> SaveNew([FromBody] ProductsFavouriteVm model)
        {
            var result = ServicesProduct.favourite(model);
            if (result.Code == 404)
            { return NotFound(result); }
            return Ok(result);
        }
        #endregion
        #region Delete : api/Product/Delete
        [HttpGet("DeleteProductFavourite")]
        public ActionResult<IResponseDTO> DeleteProductFavourite(string ApplicationUserId = "", int DiscountId = 0)
        {
            var result = ServicesProduct.DeleteProductFavourite(ApplicationUserId, DiscountId);
            if (result.Code == 404) { return NotFound(result); }
            return Ok(result);
        }
        #endregion
        [HttpGet("GetAllFavouriteUser")] 
        public ActionResult<IResponseDTO> GetAllFavouriteUser(int pageNumber = 0, int pageSize = 0,  string ApplicationUserId = "")
        {
            var result = ServicesProduct.GetAllProdcutUser(pageNumber, pageSize, ApplicationUserId);
            if (result.Code == 404) { return NotFound(result); }
            return Ok(result);
        }
    }
}
