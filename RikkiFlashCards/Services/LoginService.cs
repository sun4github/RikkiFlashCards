using Microsoft.AspNetCore.Identity;
using RikkiFlashCards.Models.DomainModels;
using RikkiFlashCards.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikkiFlashCards.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<FlashCardUser> _userManager;
        private readonly SignInManager<FlashCardUser> _signInManager;

        public LoginService(UserManager<FlashCardUser> userManager
            , SignInManager<FlashCardUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public async Task<SignInResult> ValidateLogin(LoginViewModel loginViewModel)
        {
            var fcUser = await _userManager.FindByNameAsync(loginViewModel.UserName);

            return await _signInManager.PasswordSignInAsync(fcUser, loginViewModel.Password, false, false);
        }
    }
}
