using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class StudentsDbInitializer : CreateDatabaseIfNotExists<StudentContext>
    {
        protected override void Seed(StudentContext db)
        {
            db.Students.Add(new Student { Name = "John", Surname = "Johnson", Age = 25, GPA = 10 });
            db.Students.Add(new Student { Name = "Jack", Surname = "Jackson", Age = 23, GPA = 11 });
            db.Students.Add(new Student { Name = "Nick", Surname = "Nixon", Age = 27, GPA = 9 });

            base.Seed(db);
        }
    }
}