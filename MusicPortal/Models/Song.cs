﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicPortal.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Album { get; set; }
        public List<Genre> Genres { get; set; }
        public User Publisher { get; set; }
        public int Year { get; set; }
        public string FilePath { get; set; }
    }
}