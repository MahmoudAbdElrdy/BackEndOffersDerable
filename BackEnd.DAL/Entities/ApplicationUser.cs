using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public int verficationCode { get; set; }
        public bool? confirmed { get; set; }
        public string Image { get; set; }
        [ForeignKey("tblCountries")]
        public int? tblCountriesId { get; set; }
        [ForeignKey("tblCities")]
        public int? tblCitiesId { get; set; }
        public virtual tblCountries tblCountries { get; set; }
        public virtual tblCities tblCities { get; set; }
    }
}
