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

        public UserRate Add(UserRate userRate)
        {
            context.UserRates.Add(userRate);
            context.SaveChanges();

            return userRate;
        }

        public async Task<UserRate> AddAsync(UserRate userRate)
        {
            context.UserRates.Add(userRate);
            await context.SaveChangesAsync();

            return userRate;
        }

        public UserRate Add(int userId, int titleId, int score, string review = null)
        {
            return Add(new UserRate(userId, titleId, score));
        }

        public async Task<UserRate> AddAsync(int userId, int titleId, int score, string review = null)
        {
            return await AddAsync(new UserRate(userId, titleId, score));
        }

        //+++ Update UserRate

        public UserRate Update(UserRate userRate)
        {
            //context.UserRates.Attach(userRate);
            //var entry = context.Entry(userRate);
            //entry.Property(e => e.Score).IsModified = true;
            //entry.Property(e => e.Review).IsModified = true;

            context.UserRates.Update(userRate);
            context.SaveChanges();

            return userRate;
        }

        public async Task<UserRate> UpdateAsync(UserRate userRate)
        {
            //context.UserRates.Attach(userRate);
            //var entry = context.Entry(userRate);
            //entry.Property(e => e.Score).IsModified = true;
            //entry.Property(e => e.Review).IsModified = true;

            context.UserRates.Update(userRate);
            await context.SaveChangesAsync();

            return userRate;
        }

        //+++ Delete UserRate

        public UserRate Delete(UserRate userRate)
        {
            var deletedRate = context.UserRates.Remove(userRate).Entity;
            context.SaveChanges();

            return deletedRate;
        }

        public async Task<UserRate> DeleteAsync(UserRate userRate)
        {
            var deletedRate = context.UserRates.Remove(userRate).Entity;
            await context.SaveChangesAsync();

            return deletedRate;
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
