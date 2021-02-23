using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Prodcuts
{
   public class ProductDto
    {
        public int Id { get; set; }
        public string ProdcutName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? CompanyId { get; set; }
        public List<string> ProductImages { get; set; }
    }
    public class ShowProductDto
    {
        public int Id { get; set; }
        public string ProdcutName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<string> ProductImages { get; set; }
    }
    public class ShowListProductDto 
    {
        public int Id { get; set; }
        public string ProdcutName { get; set; }
        public string DiscountDescription { get; set; }
        public string ProductDescription { get; set; }  
        public double? NewPrice { get; set; }
        public double? OldPrice { get; set; } 
        public string ProductImage  { get; set; }
        public int ProductId { get; set; }
    }
}
