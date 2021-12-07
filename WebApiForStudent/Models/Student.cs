using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.Models
{
    public class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Did FirstName write??")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Did LaststName write??")]
        public string LastName { get; set; }

        public Address Address { get; set; }

        public Standard Standard { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
