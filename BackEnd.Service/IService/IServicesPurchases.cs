using BackEnd.Service.DTO.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.IService
{
 public  interface  IServicesPurchases 
    {
        IResponseDTO Insert(PurchasesDto entity);
         IResponseDTO GetPurchasesByUserId(int pageNumber = 0, int pageSize = 0, string ApplicationUserId="");
        IResponseDTO GetAllClient(int pageNumber = 0, int pageSize = 0);
    }
}
