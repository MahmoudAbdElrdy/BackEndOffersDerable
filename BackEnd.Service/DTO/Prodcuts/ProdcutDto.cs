﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Prodcuts
{
   public class ProductDto
    {
        public string ProdcutName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? CompanyId { get; set; }
        public List<string> ProductImages { get; set; }
    }
    public class ShowProductDto
    {
        public string ProdcutName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<string> ProductImages { get; set; }
    }
}