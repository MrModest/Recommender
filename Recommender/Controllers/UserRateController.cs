using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recommender.Models;
using Recommender.Models.ViewModels;

namespace Recommender.Controllers
{
    [Authorize]
    public class UserRateController : Controller
    {
        private IUserRateRepository repository;
        private UserManager<IdentityUser<int>> userManager;

        public UserRateController(IUserRateRepository repository, UserManager<IdentityUser<int>> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Rate([FromBody] RateViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null) { return Unauthorized(); }

            if (model == null) { return NotFound(); }

            UserRate userRate = await repository.GetUserRateByUserAndTitleAsync(user.Id, model.TitleId);

            if (userRate == null || userRate?.Id == 0)
            {
                await repository.AddAsync(user.Id, model.TitleId, model.Score);
            }
            else
            {
                await repository.UpdateAsync(userRate);
            }
            
            return NoContent();
        }
    }
}
