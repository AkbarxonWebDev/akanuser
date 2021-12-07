using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.App_Data.Teacher_App
{
    public interface ITeacherService
    {
        Task<string> GetAll();

        Task<string> Get(Guid id);


        Task<bool> Delete(Guid id);

        Task<bool> Put(Guid id, WebApiForStudent.Models.Teacher teacher);

        Task<bool> Post(WebApiForStudent.Models.Teacher teacher);
    }
}
