using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Service.ISercice
{
  public interface IidentityServices
  {
        Task<IResponseDTO> RegisterAsync(string Role , string FirstName, string LastName, string Email, string Password, string Image, string PhoneNumber);
        Task<IResponseDTO> LoginAsync(string Email, string Password);
        IResponseDTO GetRoles();
        Task<Boolean> sendVerficationToEMail(string Email);
        Task<Result> verfayUser(UserVerfayRequest request);
        Task<IResponseDTO> updateresetPasswordCode(int num, string Email);
        Task<Boolean> sendCodeResetPasswordToEMail(int num, string Email);
        Task<IResponseDTO> resetPasswordVerfayCode(UserVerfayResetPasswordCode request);
        Task<IResponseDTO> ResetPassword(ResetPasswordVm resetpasswordVm);
        IResponseDTO checkPhone(string PhoneNumber);
        IResponseDTO verifyAccount(string PhoneNumber);
        IResponseDTO RestPasswordByPhone(ResetPasswordMobile resetPasswordMobile);
        IResponseDTO GetProfile(string UserId);
        IResponseDTO GetProfileClient(string UserId); 
        IResponseDTO UpdateUser(UpdateUser user);
        IResponseDTO UpdateImage(string ApplicationUserId, string Image);
    }
}
