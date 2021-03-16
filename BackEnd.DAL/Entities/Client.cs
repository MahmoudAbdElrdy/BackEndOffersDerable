using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Client:Base
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Code { get; set; }
        public string Token { get; set; } = "";
        // public string Longitude { get; set; }
        // public string Latitude { get; set; }
    }
}
