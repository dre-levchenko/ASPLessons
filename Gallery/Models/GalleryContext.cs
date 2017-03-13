using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gallery.Models
{
    public class GalleryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}