using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Client
{
 public   class NotificationClientDto
    {
        public int NotificationCid { get; set; }
        public int? ClientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public bool? IsDelete { get; set; }

    }
    public class NotificationDto 
    {
        public int NotificationCid { get; set; }
        public List<int?> Clients { get; set; } 
        public string Content { get; set; }
        public string Title { get; set; }
        public string Token { get; set; }
        public List<string> Tokens { get; set; } 
    }


}
