using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.BAL.Models
{
  public class AuthenticationResult
  {
        public string Token { get; set; }
        public string Role{ get; set; }
        public string UserType { get; set; }
      
    }
}
