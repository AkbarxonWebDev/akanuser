using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.Models
{
    public class Standard
    {
        public Standard()
        {
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }


        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}
