using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notebook.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}
