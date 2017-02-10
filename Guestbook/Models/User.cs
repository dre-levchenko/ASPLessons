using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Guestbook.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must specify a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must set a password")]
        public string Password { get; set; }
    }
}