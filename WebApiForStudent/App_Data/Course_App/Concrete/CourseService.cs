using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Models;

namespace WebApiForStudent.App_Data.Course_App.Concrete
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _db;

        public CourseService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var course = await _db.Courses.FindAsync(id);
                _db.Courses.Remove(course);
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
            var course = await _db.Courses.Include(x => x.Teacher).FirstOrDefaultAsync(x => x.Id == id);
            string JsonCourse = JsonConvert.SerializeObject
                (
                    course,Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling=ReferenceLoopHandling.Ignore
                    }
                );
            return JsonCourse;
        }

        public async Task<string> GetAll()
        {
            var courses = await _db.Courses.Include(x => x.Teacher).ToListAsync();
            string Jsoncourses = JsonConvert.SerializeObject(courses, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return Jsoncourses;
        }

        public async Task<bool> Post(Course course)
        {
                var res= new Course()
                {
                    Id = Guid.NewGuid(),
                    Name = course.Name,
                    Location = course.Location,
                    Teacher = new Teacher()
                    {
                        Id = course.Teacher.Id,
                        Name = course.Teacher.Name
                    }
                };
                _db.Entry(res).State = EntityState.Added;
                await _db.SaveChangesAsync();
                return true;

        }

        public async Task<bool> Put(Guid id, Course course)
        {
            try
            {

                var result = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
                result.Name = course.Name;
                result.Location = course.Location;
                _db.Entry(result).State = EntityState.Modified;
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
