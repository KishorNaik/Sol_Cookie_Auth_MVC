using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sol_Demo.Models;
using Sol_Demo.Repository;

namespace Sol_Demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository = null;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            Users = new UserModel();
        }

        [BindProperty]
        public UserModel Users { get; set; }

        // Login Page
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var data = await userRepository?.LoginAsync(this.Users);

            if (data is ClaimsPrincipal)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, data as ClaimsPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                });

                if (returnUrl != null && Url.IsLocalUrl(returnUrl))
                {
                    return base.LocalRedirect(returnUrl);
                }

                return base.RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.Message = data.Message;
                return View("Index");
            }
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return base.RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> OnLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return base.RedirectToAction("Login");
        }
    }
}