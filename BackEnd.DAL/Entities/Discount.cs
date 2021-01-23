using BackEnd.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Discount:Base
    {
        public DiscountType? DiscountType { get; set; }
        public double? DiscountValue { get; set; }
        public double? DiscountRate{ get; set; }
        public DateTime? SatrtDate { get; set; }
        public DateTime? EndDate{ get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
