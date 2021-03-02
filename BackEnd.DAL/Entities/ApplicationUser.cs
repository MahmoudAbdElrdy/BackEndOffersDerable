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
        public int verficationCode { get; set; }
        public bool? confirmed { get; set; }
        public bool? confirmedMobile { get; set; }
        public string FullName { get; set; }
        public int? resetPasswordCode { get; set; }
        public string Image { get; set; }
       public List<ProductFavourite> ProductFavourites { get; set; }
    }
}
