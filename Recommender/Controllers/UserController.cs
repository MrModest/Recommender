using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recommender.Infrastructure;
using Recommender.Models;
using Recommender.Models.ViewModels;

namespace Recommender.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUserRateRepository          userRateRep;
        private IMovieRepository             movieRep;
        private IHostingEnvironment          hostingEnv;

        public UserController(UserManager<ApplicationUser> userManager,
                              IUserRateRepository          userRateRep,
                              IMovieRepository             movieRep,
                              IHostingEnvironment          hostingEnv)
        {
            this.userManager = userManager;
            this.userRateRep = userRateRep;
            this.movieRep    = movieRep;
            this.hostingEnv  = hostingEnv;
        }

        public async Task<IActionResult> Index(string userName = null)
        {
            if (userName == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", new { userName = User.Identity.Name });
                }
                return NotFound(new { message = "Username is Empty." });
            }

            var user = await userManager.FindByNameAsync(userName);
            if (user == null) { return NotFound(new { mesage = $"User with name '{userName}' not found." }); }

            var userRates = await userRateRep.GetUserRatesByUser(user.Id).ToListAsync();
            var titleIds = userRates.Select(ur => ur.TitleId);
            var movies = await movieRep.Movies.Where(m => titleIds.Contains(m.Id)).ToListAsync();
            var currentUserRates = await User.GetUserRatesAsync(userRateRep);

            return View(new ProfileViewModel
            {
                User = user,
                UserMovies = movies.GetUserMovies(currentUserRates)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var model = new UserEditViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                AvatarImageUrl = user.GetAvatarPath(),
                BackgroundImageUrl = user.GetBgImagePath()
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                var foundedUserName = (model.UserName != user.UserName) ? (await userManager.FindByNameAsync(model.UserName))?.UserName : null;
                var foundedEmail    = (model.Email    != user.Email)    ? (await userManager.FindByNameAsync(model.Email))   ?.Email    : null;

                if (foundedUserName != null)
                {
                    ModelState.AddModelError("UserName", "UserName is busy.");
                    return View(model);
                }

                if (foundedEmail != null)
                {
                    ModelState.AddModelError("Email", "Email is busy.");
                    return View(model);
                }

                user.UserName = model.UserName;
                user.Email = model.Email;

                if (!String.IsNullOrWhiteSpace(model.NewPassword))
                {
                    var res = await userManager.ChangePasswordAsync(user, model.OldPassword ?? String.Empty, model.NewPassword);
                    if (!res.Succeeded)
                    {
                        res.Errors.AddErrorToModelState(ModelState);
                        return View(model);
                    }
                }

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    result.Errors.AddErrorToModelState(ModelState);
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]IFormFile file, [FromForm]string type)
        {
            if (type != "avatar" && type != "bg-image") { return StatusCode(415, new { message = "Type must be 'avatar' or 'bg-image'",  file, type }); }
            if (file == null || file.Length == 0) { return NotFound(new { message = "File not found or file size is 0." }); }
            if (!file.ContentType.Contains("image/")) { return StatusCode(415, new { message = "Only image supported" }); }
            if (file.Length > 5 * 1024 * 1024) { return StatusCode(413, new { message = "File size too large. Max size: 5 MB." }); }

            var filename = $"{User.GetUserId()}{Path.GetExtension(file.FileName)}";
            var path = $"{hostingEnv.WebRootPath}/userfiles/{type}s/{filename}";

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (type == "avatar")   { user.AvatarImagePath     = filename; }
            if (type == "bg-image") { user.BackgroundImagePath = filename; }

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Json(new { url = $"/userfiles/{type}s/{filename}" });
            }
            else
            {
                return StatusCode(500, new { errors = result.Errors.Select(e => e.Description) });
            }
        }
    }
}
