using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class tblCities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ArabicCityName { get; set; }
        public string EnglishCityName { get; set; }
        [ForeignKey("tblCountries")]
        public int CountryID { get; set; }
        public virtual tblCountries tblCountries { get; set; }
    }
}
