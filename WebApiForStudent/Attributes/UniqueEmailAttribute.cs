using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.DataLayer;

namespace WebApiForStudent.Attributes
{
    public class UniqueEmailAttribute:ValidationAttribute
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    //string email = value.ToString();
        //    //var _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
        //    //if (_context.Accounts.Any(x=>x.Email==email))
        //    //{
        //    //    return new ValidationResult("Email already exist");
        //    //}
        //    //return ValidationResult.Success;
        //}
    }
}
