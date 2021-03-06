using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.App_Data.Student
{
    public interface IStudentService
    {
        Task<string> GetAll();

        Task<string> Get(Guid id);


        Task<bool> Delete(Guid id);

        Task<bool> Put(Guid id, WebApiForStudent.Models.Student student);

        Task<bool> Post(WebApiForStudent.Models.Student student);
    }
}
