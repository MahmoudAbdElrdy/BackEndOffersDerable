using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class ProductImages:Base
    {
        public string ProductImage{ get; set; }
        public int? ProductId { get; set;}
        public Product Product { get; set; }
    }
}
