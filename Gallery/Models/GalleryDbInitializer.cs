using Gallery.Models.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gallery.Models
{
    public class GalleryDbInitializer : DropCreateDatabaseAlways<GalleryContext>
    {
        protected override void Seed(GalleryContext context)
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