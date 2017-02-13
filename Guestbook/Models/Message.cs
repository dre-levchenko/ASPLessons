using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Guestbook.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string MessageBody { get; set; }
        [Required]
        public DateTime MessageDate { get; set; }
        public User User { get; set; }
    }
}