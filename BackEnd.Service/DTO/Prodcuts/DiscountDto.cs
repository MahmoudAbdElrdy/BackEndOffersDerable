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
        public int? DiscountType { get; set; }
        public double? DiscountValue { get; set; }
        public double? DiscountRate { get; set; }
        public string DiscountDescription { get; set; }
        public DateTime? SatrtDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ProductName { get; set; }
        public int ProdcutId { get; set; }
        public string ProdcutDescription{ get; set; }
        public string ProdcutImage{ get; set; } 
        public double? ProdcutPrice{ get; set; }
        public double? ProdcutNewPrice{ get; set; } 
        // public List<CategoryDto> Categories { get; set; }
    }
}
