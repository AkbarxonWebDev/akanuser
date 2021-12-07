using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.Models
{
    public class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Standard Standard { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
