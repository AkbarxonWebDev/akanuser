using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiForStudent.App_Data.Course_App
{
    public interface ICourseService
    {
        Task<string> GetAll();

        Task<string> Get(Guid id);


        Task<bool> Delete(Guid id);

        Task<bool> Put(Guid id, WebApiForStudent.Models.Course course);

        Task<bool> Post(WebApiForStudent.Models.Course course);
    }
}
