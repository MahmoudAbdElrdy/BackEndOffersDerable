using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
 public   class ProductFavourite:Base
    {
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; } 
        public  ApplicationUser ApplicationUser { get; set; }
        
        public int? DiscountId { get; set; }
        public  Discount Discounts { get; set; }  
    }
}
