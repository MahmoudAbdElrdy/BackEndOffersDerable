using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO;
using BackEnd.Service.DTO.Prodcuts;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        #region privateFild
        private IRatingServices ServicesRating;
        #endregion

        #region RatingController(IServicesRating _ServicesRating)
        public RatingController(IRatingServices _ServicesRating)
        {
            ServicesRating = _ServicesRating;
        }
        #endregion

        #region Get : api/Rating/GetAll
        [HttpGet("GetPage")]
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesRating.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
        #region Get : api/Rating/GetAllRatingByProductId
        [HttpGet("GetAllRatingByDiscountId")]
        public IResponseDTO GetAllRatingByDiscountId(int pageNumber = 0, int pageSize = 0, int Id = 0)
        {
            var result = ServicesRating.GetAllRatingByDiscountId(pageNumber, pageSize, Id);
            return result;
        }
        #endregion
        #region Get : api/Rating/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesRating.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/Rating/Update
        [HttpPost("Update")]
        public IResponseDTO Update([FromBody]RatingDto model)
        {

            var result = ServicesRating.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/Rating/Delete
        [HttpGet("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesRating.Delete(id);
            return result;
        }
        #endregion

       
        #region Post : api/Rating/SaveNew
        [HttpPost("SaveNew")]
        public IResponseDTO SaveNew([FromBody] RatingDto model)
        {
            var result =  ServicesRating.Insert(model);
            return result;
        }
        #endregion
    }
}
