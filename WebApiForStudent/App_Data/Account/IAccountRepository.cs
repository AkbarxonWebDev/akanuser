using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.ViewModel;

namespace WebApiForStudent.App_Data.Account
{
    public interface IAccountRepository
    {
        Task<string> Register(RegisterViewModel model);

        Task<string> Login(SignInViewModel model);
    }
}
