using BackEnd.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Companies
{
  public  class CompanyDto
    {
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string CompanyDescription { get; set; }
        public UserRegisteration User { get; set; }
    }
    public class ShowCompanyDto 
    {
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string CompanyDescription { get; set; }
        public UserRegisteration User { get; set; }
    }
    public class CompanyInsertDto
    {
        public string ApplicationUserId { get; set; }
      
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string CompanyDescription { get; set; }
    }
}
