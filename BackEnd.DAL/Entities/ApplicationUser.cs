using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
        
    }
}
