using EduHome.Models.Entities;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var existUser = await _userManager.FindByNameAsync(model.Username);
            if (existUser is null)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();

            }
            var signResult = await _signInManager.PasswordSignInAsync(existUser, model.Password, model.RememberMe, true);

            if (!signResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View();
            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
