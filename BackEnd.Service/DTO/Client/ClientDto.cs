using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

    }
}
