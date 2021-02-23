using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO.Companies;
using BackEnd.Service.IService;
using BackEnd.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        #region privateFild
        private ICompanyServices ServicesCompany;
        #endregion

        #region CompanyController(IServicesCompany _ServicesCompany)
        public CompanyController(ICompanyServices _ServicesCompany)
        {
            ServicesCompany = _ServicesCompany;
        }
        #endregion

        #region Get : api/Company/GetAll
        [HttpGet("GetPage")]
        public ActionResult<IResponseDTO> GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesCompany.GetAll(pageNumber, pageSize);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion
      
        #region Get : api/Company/GetById
        [HttpGet("GetById")]
        public ActionResult<IResponseDTO> GetById(int id)
        {
            var result = ServicesCompany.GetByIdAsync(id);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion

        #region Put : api/Company/Update
        [HttpPost("Update")]
        public ActionResult<IResponseDTO> Update([FromBody]CompanyDto model)
        {

            var result = ServicesCompany.Update(model);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion

        #region Delete : api/Company/Delete
        [HttpGet("Delete")]
        public ActionResult<IResponseDTO> Delete(int id)
        {
            var result = ServicesCompany.Delete(id);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion

       
        #region Post : api/Company/SaveNew
        [HttpPost("SaveNew")]
        public async Task<ActionResult<IResponseDTO>> SaveNew([FromBody] CompanyDto model)
        {
            var result = await ServicesCompany.Insert(model);
            if (result.Code == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        #endregion
    }
}
