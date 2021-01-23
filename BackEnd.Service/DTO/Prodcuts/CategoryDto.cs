using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Prodcuts
{
 public   class CategoryDto
    {
        public string CategoryName { get; set; }
        public int Id { get; set; }
        public string ParentCategoryName { get; set; }
        public int? ParentId { get; set; }
    }
}
