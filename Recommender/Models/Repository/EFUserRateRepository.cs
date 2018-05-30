using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recommender.Infrastructure;

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

            //Prediction.RateItem(userRate.UserId, userRate.TitleId, userRate.Score);

            return userRate;
        }

        public async Task<UserRate> AddAsync(UserRate userRate)
        {
            context.UserRates.Add(userRate);
            await context.SaveChangesAsync();

            //await Prediction.RateItemAsync(userRate.UserId, userRate.TitleId, userRate.Score);

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
            context.UserRates.Update(userRate);
            context.SaveChanges();

            //Prediction.RateItem(userRate.UserId, userRate.TitleId, userRate.Score);

            return userRate;
        }

        public async Task<UserRate> UpdateAsync(UserRate userRate)
        {
            context.UserRates.Update(userRate);
            await context.SaveChangesAsync();

            //await Prediction.RateItemAsync(userRate.UserId, userRate.TitleId, userRate.Score);

            return userRate;
        }

        //+++ Delete UserRate

        public UserRate Delete(UserRate userRate)
        {
            var deletedRate = context.UserRates.Remove(userRate).Entity;
            context.SaveChanges();

            //Prediction.DeleteItemRate(userRate.UserId, userRate.TitleId);

            return deletedRate;
        }

        public async Task<UserRate> DeleteAsync(UserRate userRate)
        {
            var deletedRate = context.UserRates.Remove(userRate).Entity;
            await context.SaveChangesAsync();

            //await Prediction.DeleteItemRateAsync(userRate.UserId, userRate.TitleId);

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
