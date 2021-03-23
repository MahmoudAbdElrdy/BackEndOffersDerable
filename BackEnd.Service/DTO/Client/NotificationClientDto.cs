using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Client
{
 public   class NotificationClientDto
    {
        public int NotificationCid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
      
       

    }
    public class NotificationDto 
    {
        public int NotificationCid { get; set; }
        public List<int?> Clients { get; set; } 
        public string Content { get; set; }
        public string Title { get; set; }
        public string Token { get; set; }
        public List<string> Tokens { get; set; }
        public DateTime? CreationDate { get; set; }
    }


}
