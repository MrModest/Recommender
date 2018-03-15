using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Recommender.Models;
using Recommender.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Recommender.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser<int>> userManager;
        private SignInManager<IdentityUser<int>> signInManager;
        private IUserRateRepository userRateRep;
        private IMovieRepository movieRep;

        public AccountController(UserManager<IdentityUser<int>>   userManager, 
                                 SignInManager<IdentityUser<int>> signInManager, 
                                 IUserRateRepository              userRateRep,
                                 IMovieRepository                 movieRep)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRateRep = userRateRep;
            this.movieRep = movieRep;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser<int> user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Movie");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login or password");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterModel {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser<int> user = new IdentityUser<int> { Email = model.Email, UserName = model.UserName };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return Redirect(model?.ReturnUrl ?? "/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Profile(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null) { return NotFound(); }

            var userRates = await userRateRep.GetUserRatesByUser(user.Id).ToListAsync();

            //var movies = await movieRep.Movies.Where(m => userRates.Select(ur => ur.TitleId).Contains(m.Id)).ToListAsync();

            var movies = await movieRep.Movies.Take(10).ToListAsync();

            return View(new ProfileViewModel {
                User = user,
                UserRates = userRates,
                UserMovies = movies
            });
        }
    }
}