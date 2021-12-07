using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.Models
{
    public class Address
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Address_1 { get; set; }

        [MaxLength(100)]
        public string Address_2 { get; set; }

        [MaxLength(20)]
        public string City { get; set; }

        [MaxLength(20)]
        public string State { get; set; }

    }
}
