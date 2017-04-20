using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Notebook.Models
{
    public static class NotebookDbInitializer
    {
        public static void Initialize(NotebookContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Notes.Any())
            {
                context.Notes.Add(new Note()
                {
                    Body = "Initial",
                    Date = DateTime.Now
                });
                context.SaveChanges();
            }
        }
    }
}
