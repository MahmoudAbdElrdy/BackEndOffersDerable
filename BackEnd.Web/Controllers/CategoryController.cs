using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO.Prodcuts;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region privateFild
        private ICategoryServices ServicesCategory;
        #endregion

        #region CategoryController(IServicesCategory _ServicesCategory)
        public CategoryController(ICategoryServices _ServicesCategory)
        {
            ServicesCategory = _ServicesCategory;
        }
        #endregion

        #region Get : api/Category/GetAll
        [HttpGet("GetPage")]
        public ActionResult<IResponseDTO> GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesCategory.GetAll(pageNumber, pageSize);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
          
        }
        #endregion
      
        #region Get : api/Category/GetById
        [HttpGet("GetById")]
        public ActionResult<IResponseDTO> GetById(int id)
        {
            var result = ServicesCategory.GetByIdAsync(id);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion

        #region Put : api/Category/Update
        [HttpPost("Update")]
        public ActionResult<IResponseDTO> Update([FromBody]CategoryDto model)
        {

            var result = ServicesCategory.Update(model);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion

        #region Delete : api/Category/Delete
        [HttpGet("Delete")]
        public ActionResult<IResponseDTO> Delete(int id)
        {
            var result = ServicesCategory.Delete(id);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion

       
        #region Post : api/Category/SaveNew
        [HttpPost("SaveNew")]
        public ActionResult<IResponseDTO> SaveNew([FromBody] CategoryDto model)
        {
            var result =  ServicesCategory.Insert(model);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion
    }
}
