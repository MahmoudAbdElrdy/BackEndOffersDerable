using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Category:Base
    {
        public string CategoryName{ get; set; }
        public int? ParentId { get; set; }
        public  Category Parent { get; set; }
        // public string Image{ get; set; } 
        public ICollection<Product> Products { get; set; }
    }
}
