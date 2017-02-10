using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guestbook.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageBody { get; set; }
        public DateTime MessageDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}