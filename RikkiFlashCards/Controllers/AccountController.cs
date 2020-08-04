using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RikkiFlashCards.Models.ViewModels;
using RikkiFlashCards.Services;

namespace RikkiFlashCards.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;

        public AccountController(ILoginService loginService)
        {
            this._loginService = loginService;
        }
        public ViewResult Login(string returnUrl)
        {
            var newLogin = new LoginViewModel { ReturnUrl = returnUrl };
            return View(newLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var res = await _loginService.IsLoginSuccessful(loginViewModel);
            if(res.Succeeded)
            {
                return Redirect(loginViewModel.ReturnUrl);
            }
            else
            {
                return View();
            }
        }
    }
}