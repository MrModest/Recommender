using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public interface IUserRateRepository
    {
        IQueryable<UserRate> UserRates { get; }

        void Add(UserRate userRate);
        Task AddAsync(UserRate userRate);

        void Add(int userId, int titleId, int score, string review = null);
        Task AddAsync(int userId, int titleId, int score, string review = null);

        void Update(UserRate userRate);
        Task UpdateAsync(UserRate userRate);

        IQueryable<UserRate> GetUserRatesByUser(int userId);

        UserRate GetUserRateByUserAndTitle(int userId, int titleId);
        Task<UserRate> GetUserRateByUserAndTitleAsync(int userId, int titleId);
    }
}
