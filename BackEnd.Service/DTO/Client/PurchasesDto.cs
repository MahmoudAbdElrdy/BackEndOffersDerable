using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Client
{
  public  class PurchasesDto
    {
        public string ApplicationUserId { get; set; }
        public int? DiscountId { get; set; }
        public string PhoneNumber { get; set; } 
        public double? NewPrice { get; set; } 
    }
    public class ShowPurchasesDto 
    {
        public string ApplicationUserId { get; set; }
        public int? DiscountId { get; set; }
        public double? quantity { get; set; }
        public string RandomCode { get; set; }
        public string CompanyName { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? SaleDate { get; set; }
        public double? NewPrice { get; set; }
        public string ProductName { get; set; } 
        public string PhoneNumber { get; set; }

    }
}
