﻿using IdentityNetCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNetCore.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Signup()
        {
            var model = new SignupViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    var user = new IdentityUser
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                        return RedirectToAction("Signin");

                    ModelState.AddModelError("Signup", string.Join("<br />", result.Errors.Select(x => x.Description)));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Signin()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
