using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public  class Purchases:Base
    {
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
