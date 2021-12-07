using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiForStudent.App_Data.Account;
using WebApiForStudent.ViewModel;

namespace WebApiForStudent.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signIn;
        private readonly IAccountRepository account;

        public AccountsController(IAccountRepository _account, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signIn)
        {
            account = _account;
            userManager = _userManager;
            signIn = _signIn;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new IdentityUser()
            {
                UserName = model.Username,
                Email = model.Email
            };
            var result = await userManager.CreateAsync(response, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Username = result });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await account.Login(model);
            if (result.Contains("Username"))
            {
                return BadRequest(result);
            }
            return Ok(new { Username = result });
        }
        [HttpPost("Logout")]
        public string Logout()
        {
            return "For User Logout";
        }
    }
}
