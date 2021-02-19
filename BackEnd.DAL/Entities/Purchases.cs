using BackEnd.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public  class Purchases:Base
    {
        public int? ClientId { get; set; }
        public double? quantity { get; set; }
        public Client Client { get; set; }
        public DiscountType? DiscountType { get; set; }
        public double? DiscountValue { get; set; } 
        public double? DiscountRate { get; set; }
        public string DiscountDescription { get; set; }
        public DateTime? SatrtDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public double? NewPrice { get; set; }
        public int? DiscountId { get; set; } 

    }
}
