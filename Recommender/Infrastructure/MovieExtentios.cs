using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recommender.Models;
using Recommender.Models.ViewModels;

namespace Recommender.Infrastructure
{
    public static class MovieExtentios
    {
        public static List<UserMovie> GetRecommendationByUserId(this IMovieRepository rep, int userId, int maxCount = 10)
        {
            var recommendations = Prediction.GetRecommendation(userId, maxCount);
            var movies = rep.Movies.Where(m => recommendations.Select(r => r.ItemId).Contains(m.Id.ToString()));
            var uMovies = new List<UserMovie>();
            foreach (var item in recommendations)
            {
                var movie = movies.FirstOrDefault(m => m.Id.ToString() == item.ItemId);
                uMovies.Add(new UserMovie{
                    Movie = movie,
                    PredictedScore = item.Score
                });
            }

            return uMovies;
        }

        public static async Task<List<UserMovie>> GetRecommendationByUserIdAsunc(this IMovieRepository rep, int userId, int maxCount = 10)
        {
            var recommendations = await Prediction.GetRecommendationAsync(userId, maxCount);
            var movies = rep.Movies.Where(m => recommendations.Select(r => r.ItemId).Contains(m.Id.ToString()));
            var uMovies = new List<UserMovie>();
            foreach (var item in recommendations)
            {
                var movie = movies.FirstOrDefault(m => m.Id.ToString() == item.ItemId);
                uMovies.Add(new UserMovie
                {
                    Movie = movie,
                    PredictedScore = item.Score
                });
            }

            return uMovies;
        }
    }
}
