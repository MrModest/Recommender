using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Recommender.Models;
using Recommender.Models.ViewModels;

namespace Recommender.Controllers
{
    [Authorize]
    public class UserRateController : Controller
    {
        private IUserRateRepository userRateRep;
        private IMovieRepository movieRep;
        private UserManager<ApplicationUser> userManager;

        public UserRateController(IUserRateRepository userRateRep, IMovieRepository movieRep, UserManager<ApplicationUser> userManager)
        {
            this.userRateRep = userRateRep;
            this.movieRep = movieRep;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Rate([FromBody] RateViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null) { return Unauthorized(); }

            if (model == null) { return NotFound(); }

            UserRate userRate = await userRateRep.GetUserRateByUserAndTitleAsync(user.Id, model.TitleId);

            if (userRate == null || userRate?.Id == 0)
            {
                await userRateRep.AddAsync(user.Id, model.TitleId, model.Score);
            }
            else
            {
                userRate.Score = model.Score;
                await userRateRep.UpdateAsync(userRate);
            }
            
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm][JsonProperty("title_id")] int titleId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null) { return Unauthorized(); }
            if (titleId == 0) { return NotFound(new { message = $"Not fount title with id {titleId}" }); }

            UserRate userRate = await userRateRep.GetUserRateByUserAndTitleAsync(user.Id, titleId);

            if (userRate == null) { return NotFound(new { message = $"Not fount user rate for title with id {titleId}" }); }

            await userRateRep.DeleteAsync(userRate);

            var title = await movieRep.Movies.FirstOrDefaultAsync(m => m.Id == titleId);

            return Json(new { score = title.VoteAverage });
        }
    }
}
