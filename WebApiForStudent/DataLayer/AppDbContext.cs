using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiForStudent.Models;

namespace WebApiForStudent.DataLayer
{
    //step - 5 Inherit IdentityDbContext
    public class AppDbContext:IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
           
        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Address> Addresses { get; set; }

    }
}
