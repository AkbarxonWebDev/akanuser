using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.Models
{
    public class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
        }


        public Guid Id { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        public Guid TeacherId { get; set; }

        public ICollection<Student> Students { get; set; }

        public Teacher Teacher { get; set; }
    }
}
