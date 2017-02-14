using Guestbook.Models.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicPortal.Models
{
    public class MusicPortalDbInitializer : DropCreateDatabaseAlways<MusicPortalContext>
    {
        protected override void Seed(MusicPortalContext context)
        {
            context.Users.Add(new User
            {
                Name = "admin",
                Password = MD5Hasher.ComputeHash("admin")
            });

            base.Seed(context);
        }
    }
}