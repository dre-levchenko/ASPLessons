using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guestbook.Models.ViewModels
{
    public class MessagesModel
    {
        public Message NewMessage { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}