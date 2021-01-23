using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public  class Review : Base
    {
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public string ReviewText{ get; set; }
        public int? Reviewcount { get; set; }
        public int? ApplicationUserId { get; set; }
        public  ApplicationUser ApplicationUser { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
