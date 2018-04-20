using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public interface IUserRateRepository
    {
        IQueryable<UserRate> UserRates { get; }

        UserRate Add(UserRate userRate);
        Task<UserRate> AddAsync(UserRate userRate);

        UserRate Add(int userId, int titleId, int score, string review = null);
        Task<UserRate> AddAsync(int userId, int titleId, int score, string review = null);

        UserRate Update(UserRate userRate);
        Task<UserRate> UpdateAsync(UserRate userRate);

        UserRate Delete(UserRate userRate);
        Task<UserRate> DeleteAsync(UserRate userRate);

        IQueryable<UserRate> GetUserRatesByUser(int userId);

        UserRate GetUserRateByUserAndTitle(int userId, int titleId);
        Task<UserRate> GetUserRateByUserAndTitleAsync(int userId, int titleId);
    }
}
