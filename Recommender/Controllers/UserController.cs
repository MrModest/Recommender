using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private IUserRateRepository userRateRep;
        private IMovieRepository movieRep;

        public UserController(UserManager<ApplicationUser> userManager,
                              IUserRateRepository          userRateRep,
                              IMovieRepository             movieRep)
        {
            this.userManager = userManager;
            this.userRateRep = userRateRep;
            this.movieRep = movieRep;
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
    }
}
