using BackEnd.Service.DTO;
using BackEnd.Service.DTO.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
   public interface IServicesNotificationCustomer
    {
        Task<IResponseDTO> InsertAsync(NotificationClientDto entity);
        IResponseDTO GetAll();
        IResponseDTO GetAvailable();
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO GetByClient(int id); 
         IResponseDTO Delete(int id);
        IResponseDTO Update(NotificationClientDto entity);
        IResponseDTO Remove(NotificationClientDto entity);
        Task<IResponseDTO> InsertAsync(NotificationDto model);
    }
}
