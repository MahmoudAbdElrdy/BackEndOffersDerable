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
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesCompany.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
      
        #region Get : api/Company/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesCompany.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/Company/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]CompanyDto model)
        {

            var result = ServicesCompany.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/Company/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesCompany.Delete(id);
            return result;
        }
        #endregion

       
        #region Post : api/Company/SaveNew
        [HttpPost("SaveNew")]
        public Task<IResponseDTO> SaveNew([FromBody] CompanyDto model)
        {
            var result =  ServicesCompany.Insert(model);
            return result;
        }
        #endregion
    }
}
