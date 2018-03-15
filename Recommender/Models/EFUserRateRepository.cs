using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class EFUserRateRepository : IUserRateRepository
    {
        private TMDbContext context;

        public EFUserRateRepository(TMDbContext context)
        {
            this.context = context;
        }

        public IQueryable<UserRate> UserRates => context.UserRates;

        //+++ Add UserRate

        public void Add(UserRate userRate)
        {
            context.UserRates.Add(userRate);
            context.SaveChanges();
        }

        public async Task AddAsync(UserRate userRate)
        {
            context.UserRates.Add(userRate);
            await context.SaveChangesAsync();
        }

        public void Add(int userId, int titleId, int score, string review = null)
        {
            Add(new UserRate(userId, titleId, score));
        }

        public async Task AddAsync(int userId, int titleId, int score, string review = null)
        {
            await AddAsync(new UserRate(userId, titleId, score));
        }

        //+++ Update UserRate

        public void Update(UserRate userRate)
        {
            context.UserRates.Update(userRate);
            context.SaveChanges();
        }

        public async Task UpdateAsync(UserRate userRate)
        {
            context.UserRates.Update(userRate);
            await context.SaveChangesAsync();
        }

        //+++ Help Methods

        public IQueryable<UserRate> GetUserRatesByUser(int userId)
        {
            return context.UserRates.Where(ur => ur.UserId == userId);
        }

        public UserRate GetUserRateByUserAndTitle(int userId, int titleId)
        {
            return context.UserRates.FirstOrDefault(ur => ur.UserId == userId && ur.TitleId == titleId);
        }

        public async Task<UserRate> GetUserRateByUserAndTitleAsync(int userId, int titleId)
        {
            return await context.UserRates.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.TitleId == titleId);
        }
    }
}
