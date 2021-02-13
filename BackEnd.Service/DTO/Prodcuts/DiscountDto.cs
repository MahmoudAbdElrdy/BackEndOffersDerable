using BackEnd.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Prodcuts
{
  public  class DiscountDto
    {
        public int Id { get; set; }
        public int? DiscountType { get; set; }
        public double? DiscountValue { get; set; }
        public double? DiscountRate { get; set; }
        public string DiscountDescription { get; set; }
        public DateTime? SatrtDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ProductId { get; set; }
    }
    public class ShowDiscountDto 
    {
        public int Id { get; set; }
        public double? DiscountRate { get; set; }
        public string DiscountDescription { get; set; }
        public double? NumberDays { get; set; }
        public string ProductName { get; set; }
        public int? ProductId { get; set; }
        public string ProductDescription{ get; set; } 
        public double? NewPrice { get; set; }
        public double? OldPrice { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
       
        public string CompanyLogo { get; set; } 
        public string CompanyPhoneNumber { get; set; } 
        public List<string> ProductImages { get; set; }
        public string CompanyDescription { get; set; }
        // public List<CategoryDto> Categories { get; set; }
    }
}
