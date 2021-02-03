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
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesProduct.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
      
        #region Get : api/Product/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesProduct.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/Product/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]ProductDto model)
        {

            var result = ServicesProduct.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/Product/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesProduct.Delete(id);
            return result;
        }
        #endregion

       
        #region Post : api/Product/SaveNew
        [HttpPost("SaveNew")]
        public IResponseDTO SaveNew([FromBody] ProductDto model)
        {
            var result =  ServicesProduct.Insert(model);
            return result;
        }
        #endregion
    }
}
