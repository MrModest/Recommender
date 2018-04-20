using Recommender.Models;
using Recommender.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Infrastructure
{
    public static class IEnumerableExtentions
    {
        public static UserRate GetUserRateByMovie(this IEnumerable<UserRate> userRates, int title_id)
        {
            return userRates?.FirstOrDefault(ur => ur.TitleId == title_id);
        }

        public static IEnumerable<UserMovie> GetUserMovies(this IEnumerable<Movie> movies, IEnumerable<UserRate> userRates = null)
        {
            foreach (var movie in movies)
            {
                yield return new UserMovie(movie, userRates?.GetUserRateByMovie(movie.Id)?.Score ?? 0 );
            }
        }
    }
}
