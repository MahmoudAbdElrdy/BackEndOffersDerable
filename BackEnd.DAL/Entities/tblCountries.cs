using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.DAL.Entities
{
    public class tblCountries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ArabicCountryName { get; set; }
        public string EnglishCountryName { get; set; }
        public virtual List<tblCities> tblCities { get; set; }
    }
}
