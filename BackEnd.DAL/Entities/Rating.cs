using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Rating:Base
    {
        public int? DiscountId { get; set; } 
        public Discount Discount { get; set; } 
        public int? ClientId { get; set; } 
        public Client Client{ get; set; }
        public double? RatingValue { get; set; }
        public string RatingText { get; set; }
    }
}
