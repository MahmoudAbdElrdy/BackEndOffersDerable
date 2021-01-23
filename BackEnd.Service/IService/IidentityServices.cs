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
        Task<AuthenticationResult> LoginAsync(string Email, string Password);
        IResponseDTO GetRoles();

    }
}
