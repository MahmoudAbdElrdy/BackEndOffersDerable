using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class Product : Base
    {
        public string ProdcutName { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Review> Reviews{ get; set; }
        public ICollection<ProductImages> ProductImages { get; set; }
      
    }
}
