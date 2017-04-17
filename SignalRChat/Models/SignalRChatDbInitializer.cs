using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SignalRChat.Models
{
    public class SignalRChatDbInitializer : DropCreateDatabaseAlways<SignalRChatContext>
    {
        protected override void Seed(SignalRChatContext context)
        {
            base.Seed(context);
        }
    }
}