using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicPortal.Models.ViewModels
{
    public class GenresModel
    {
        public Genre NewGenre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}