using Microsoft.AspNetCore.Identity;
using RikkiFlashCards.Models.ViewModels;
using System.Threading.Tasks;

namespace RikkiFlashCards.Services
{
    public interface ILoginService
    {
        Task<SignInResult> IsLoginSuccessful(LoginViewModel loginViewModel);
    }
}