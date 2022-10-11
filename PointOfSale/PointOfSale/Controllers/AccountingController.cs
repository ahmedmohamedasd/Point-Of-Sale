using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Models;
using PointOfSale.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Controllers
{
    [AllowAnonymous]
    public class AccountingController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AccountingController(SignInManager<ApplicationUser> signInManager,
                                    UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser signedUser = await userManager.FindByEmailAsync(model.Email);
                if (signedUser == null)
                {
                    ModelState.AddModelError("", "Email Or Password is incorrect");
                    return View(model);
                }
                if(signedUser.IsActive == true)
                {
                    var result = await signInManager.PasswordSignInAsync(
                                       signedUser.Email, model.Password, model.RememberMe,false
                                       );
                    if (result.Succeeded)
                    {
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return RedirectToAction("Login", "Accounting");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Products");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Or Password is incorrect");

                    }

                }
                else
                {
                    ModelState.AddModelError("", "Email is not Active");
                }
                

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Accounting");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
