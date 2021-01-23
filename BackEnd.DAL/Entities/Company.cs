using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public  class Company:Base
    {
        public int? ApplicationUserId{ get; set; }
        public  ApplicationUser User { get; set; }
      
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
