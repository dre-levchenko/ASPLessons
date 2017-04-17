using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SenderName { get; set; }
    }
}