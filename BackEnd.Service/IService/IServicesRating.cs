using BackEnd.Service.DTO;
using BackEnd.Service.DTO.Prodcuts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface IRatingServices 
    {
        IResponseDTO Insert(RatingDto entity);
        IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetAllRatingByDiscountId(int pageNumber = 0, int pageSize = 0, int DiscountId = 0);
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO Delete(int id);
        IResponseDTO Update(RatingDto entity);
        IResponseDTO Remove(RatingDto entity);
    }
}
