﻿using BackEnd.Service.DTO.Prodcuts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.IService
{
   public interface IServiceDiscount
    {
        IResponseDTO Insert(DiscountDto entity);
        IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO Delete(int id);
        IResponseDTO Update(DiscountDto entity);
        IResponseDTO Remove(DiscountDto entity);
    }
}