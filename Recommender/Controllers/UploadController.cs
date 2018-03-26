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
using Recommender.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Recommender.Controllers
{
    public class UploadController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public UploadController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserAvatarImage(IFormFile file)
        {
            if (file == null || file.Length == 0) { return NotFound(new { message = "File not found or file size is 0." }); }
            if (!file.ContentType.Contains("image/")) { return StatusCode(415, new { message = "Only image supported" }); }
            if (file.Length > 5 * 1024 * 1024) { return StatusCode(413, new { message = "File size too large. Max size: 5 MB." }); }

            string path = "/userfiles/" + file.FileName;

            using (var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            user.AvatarImagePath = "~" + path;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("User", "Index");
            }
            else
            {
                return StatusCode(500, new { errors = result.Errors.Select(e => e.Description) });
            }
        }
    }
}
