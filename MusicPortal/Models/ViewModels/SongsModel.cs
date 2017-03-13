using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicPortal.Models.ViewModels
{
    public class SongsModel
    {
        public Song NewSong { get; set; }
        public IEnumerable<HttpPostedFileBase> FileUpload { get; set; }

    }
}