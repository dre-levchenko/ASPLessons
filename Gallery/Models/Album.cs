using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gallery.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Image> Images { get; set; }
        public User Owner { get; set; }
    }
}