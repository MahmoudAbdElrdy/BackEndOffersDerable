using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Prodcuts
{
  public  class RatingDto
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? DiscountId { get; set; }
        public int? ClientId { get; set; }
        public double? RatingValue { get; set; }
        public string RatingText { get; set; }
    }
    public class ShowRatingDto 
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? DiscountId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName  { get; set; }
        public double? RatingValue { get; set; }
        public string RatingText { get; set; }
    }
}
