﻿using Microsoft.EntityFrameworkCore;
using Recommender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recommender.Infrastructure
{
    public static class IdentityExtensions
    {

        public static int GetUserId(this ClaimsPrincipal user)
        {
            if (user == null) { return -1; }
            //if (int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier).Value, out int userId))
            //{
            //    return userId;
            //}
            //return 0;
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static async Task<IEnumerable<UserRate>> GetUserRatesAsync(this ClaimsPrincipal user, IUserRateRepository userRateRep)
        {
            if (user.Identity.IsAuthenticated)
            {
                var userId = user.GetUserId();

                if (userId <= 0) { return null; }

                return await userRateRep.GetUserRatesByUser(userId).ToListAsync();
            }
            return null;
        }

    }
}
