using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Models;

namespace WebApiForStudent.App_Data.Student.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _db;

        public StudentService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var student = await _db.Students.FindAsync(id);
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<string> Get(Guid id)
        {
            var student = await _db.Students.Include(x=>x.Address).Include(y=>y.Standard).FirstOrDefaultAsync(x=>x.Id==id);
            string JsonStudent = JsonConvert.SerializeObject
                (
                student,Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling=ReferenceLoopHandling.Ignore
                }
                );
            return JsonStudent;
        }

        public async Task<string> GetAll()
        {
            var students = await _db.Students.Include(x=>x.Address).Include(y=>y.Standard).ToListAsync();
            string JsonStudent = JsonConvert.SerializeObject
                (
                students, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
                );
            return JsonStudent;
        }

        public async Task<bool> Post(Models.Student student)
        {
            try
            {
                _db.Students.Add(new Models.Student()
                {
                    Id = Guid.NewGuid(),
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Address = new Models.Address()
                    {
                        Id = Guid.NewGuid(),
                        Address_1 = student.Address.Address_1,
                        Address_2 = student.Address.Address_2,
                        City = student.Address.City,
                        State = student.Address.State
                    },
                    Standard = new Standard()
                    {
                        Id = Guid.NewGuid(),
                        Name = student.Standard.Name,
                        Discription = student.Standard.Discription
                    }

                });
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<bool> Put(Guid id, Models.Student student)
        {
            try
            {
                var res = await _db.Students.FirstOrDefaultAsync(x => x.Id == id);
                res.FirstName = student.FirstName;
                res.LastName = student.LastName;
                _db.Entry(res).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
