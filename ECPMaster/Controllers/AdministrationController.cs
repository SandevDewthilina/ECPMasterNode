using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECPMaster.Models;
using ECPMaster.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ECPMaster.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostApplicationLifetime _appLifetime;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IHostApplicationLifetime appLifetime)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _appLifetime = appLifetime;
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        public void Kill()
        {
            _appLifetime.StopApplication();
            //if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    runCron();
            //}
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole {Name = model.RoleName};
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public async Task<IActionResult> EditRole(String id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in await _userManager.Users.ToListAsync())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(String roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(String roleId, List<UserRoleViewModel> model)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult results;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    results = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    results = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (results.Succeeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }

                    return RedirectToAction("EditRole", new {Id = roleId});
                }
            }

            return RedirectToAction("EditRole", new {Id = roleId});
        }
        
        public async Task<IActionResult> DeleteRole(string id)
        {
            var user = await _roleManager.FindByIdAsync(id);
            if (user != null)
            {
                var results = await _roleManager.DeleteAsync(user);

                if (results.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return RedirectToAction("ListRoles");
        }
        
        public IActionResult ListUsers()
        {
            return View(_userManager.Users);
        }
        
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var editUserViewModel = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Name,
                Roles = await _userManager.GetRolesAsync(user)
            };
            
            return View(editUserViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
                user.Name = model.UserName;

                var results = await _userManager.UpdateAsync(user);

                if (results.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var results = await _userManager.DeleteAsync(user);

                if (results.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in results.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return RedirectToAction("ListUsers");
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Account", new {email = user.Email, token = token}, Request.Scheme);
                return Redirect(passwordResetLink);
            }

            return Json("user not found");
        }
    }
}