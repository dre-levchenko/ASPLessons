using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Guestbook.Models
{
    public class GuestbookDbInitializer : DropCreateDatabaseAlways<GuestbookContext>
    {
        protected override void Seed(GuestbookContext context)
        {
            base.Seed(context);
        }
    }
}