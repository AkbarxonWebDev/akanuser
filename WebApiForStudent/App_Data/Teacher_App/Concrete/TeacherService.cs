using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebApiForStudent.DataLayer;
using WebApiForStudent.Models;

namespace WebApiForStudent.App_Data.Teacher_App.Concrete
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _db;

        public TeacherService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var res = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
                _db.Remove(res);
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
            var res = await _db.Teachers.Include(x=>x.Standard).FirstOrDefaultAsync(x => x.Id == id);
            string JsonRes = JsonConvert.SerializeObject
                (
                    res,Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling=ReferenceLoopHandling.Ignore
                    }
                );
            return JsonRes;
        }

        public async Task<string> GetAll()
        {
            var res = await _db.Teachers.Include(x => x.Standard).ToListAsync();
            string JsonRes = JsonConvert.SerializeObject
                (
                    res, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );
            return JsonRes;
        }

        public async Task<bool> Post(Teacher teacher)
        {
            try
            {
                _db.Add(new Teacher()
                {
                    Id=Guid.NewGuid(),
                    Name=teacher.Name,
                    Standard=new Standard()
                    {
                        Id=teacher.Standard.Id,
                        Name=teacher.Standard.Name,
                        Discription=teacher.Standard.Discription
                    }
                });
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Put(Guid id, Teacher teacher)
        {
            try
            {
                var res = await _db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
                res.Name = teacher.Name;
                _db.Entry(res).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
