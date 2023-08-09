using System;
using System.Threading.Tasks;
using ECPMaster.Models;
using ECPMaster.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECPMaster.Controllers
{
     public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.Email, 
                    Email = registerViewModel.Email,
                    Name = registerViewModel.Username
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.NewPassword);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    } 
                    if (_signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, [FromQuery]string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl)) return LocalRedirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordViewModel() {Email = email, token = token});
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var results = await _userManager.ResetPasswordAsync(user, model.token, model.NewPassword);
                    if (results.Succeeded)
                    {
                        if (user.Email.Equals(User.Identity.Name))
                        {
                            return RedirectToAction("Logout");
                        }
                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var error in results.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
            return View(model);
        }
    }
}
