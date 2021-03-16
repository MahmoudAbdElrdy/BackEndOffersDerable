using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class UserRegisterationRequest
  {
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string Roles { get; set; }
  }
    public class UserRegisteration
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
        public string Role{ get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string Token { get; set; } = "";
    }
    public class UpdateUser
    {
        // public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
      //  public string Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string ApplicationUserId { get; set; }
        public string Password { get; set; }

        // public string Image { get; set; }
    }
    public class ClientTokenDto
    {
        public string ApplicationUserId { get; set; }
        public string Token { get; set; } = "";
        public int? Id { get; set; }

    }
}
