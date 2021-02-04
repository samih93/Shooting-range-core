using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shooting_range_core.Models;
using Shooting_range_core.Models.ViewModels;

namespace Shooting_range_core.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, false);
                if (result.Succeeded)
                {
                    TempData["message"] = NotificationSystem.AddMessage("تم تسجيل الدخول  بنجاح ", NotificationType.Success.Value);

                    return RedirectToAction("Index", "Tournaments");
                }

                ModelState.AddModelError(string.Empty, "كلمة المرور او البريد الاكتروني غير صحيح");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
        
    }
}