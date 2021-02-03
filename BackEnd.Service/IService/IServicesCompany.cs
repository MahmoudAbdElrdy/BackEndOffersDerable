using BackEnd.Service.DTO;
using BackEnd.Service.DTO.Companies;
using BackEnd.Service.DTO.Prodcuts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface ICompanyServices 
    {
         Task<IResponseDTO> Insert(CompanyDto entity);
        IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO Delete(int id);
        IResponseDTO Update(CompanyDto entity);
        IResponseDTO Remove(CompanyDto entity);
    }
}
