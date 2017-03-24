using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicPortal.Models.ViewModels
{
    public class SongModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Album { get; set; }
        public IList<Genre> Genres { get; set; }
        public int Year { get; set; }
    }
}