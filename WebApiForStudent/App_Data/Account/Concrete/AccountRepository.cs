using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.App_Data.Account;
using WebApiForStudent.ViewModel;

namespace WebApiForStudent.App_Data
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<IdentityUser> user;
        private readonly SignInManager<IdentityUser> signIn;
        public AccountRepository(UserManager<IdentityUser> _user, SignInManager<IdentityUser> _signIn)
        {
            user = _user;
            signIn = _signIn;
        }

        public async Task<string> Login(SignInViewModel model)
        {
            var result = await signIn.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (!result.Succeeded)
            {
                return "Invalid Username or Email";
            }
            return model.Username;
        }

        public async Task<string> Register(RegisterViewModel model)
        {
            var response = new IdentityUser()
            {
                UserName = model.Username,
                Email = model.Email
            };
            var result=await user.CreateAsync(response, model.Password);
            if (!result.Succeeded)
            {
                return result.Errors.ToString();
            }
            return model.Username;
        }
    }
}
