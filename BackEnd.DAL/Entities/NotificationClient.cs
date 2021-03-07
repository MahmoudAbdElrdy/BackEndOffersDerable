using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public  class NotificationClient
    {
        public int NotificationCid { get; set; }
        public int? ClientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Notes { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public bool IsDelete { get; set; }

        public Client Client { get; set; }
    }
}
