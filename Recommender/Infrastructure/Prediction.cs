using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PredictionIONet.Clients;
using Recommender.Models;

namespace Recommender.Infrastructure
{
    public static class Prediction
    {
        private static EventClient eventClient = new EventClient("http://127.0.0.1:7070", 1, "access_key");
        private static EngineClient engineClient = new EngineClient("http://127.0.0.1:7070", "access_key");

        public static async Task UpdateUserList(UserManager<ApplicationUser> userManager)
        {
            foreach (var userId in userManager.Users.Select(u => u.Id))
            {
                await eventClient.SetUserAsync(userId.ToString());
            }
        }

        public static async Task UpdateItemList(IMovieRepository rep)
        {
            foreach (var movieId in rep.Movies.Select(m => m.Id))
            {
                await eventClient.SetItemAsync(movieId.ToString(), new[] { "movie" });
            }
        }

        public static void RateItem(int userId, int itemId, int score)
        {
            eventClient.RateItem(userId.ToString(), itemId.ToString(), score);
        }

        public static async Task RateItemAsync(int userId, int itemId, int score)
        {
            await eventClient.RateItemAsync(userId.ToString(), itemId.ToString(), score);
        }

        public static void DeleteItemRate(int userId, int itemId)
        {
            eventClient.SetActionItem(userId.ToString(), itemId.ToString(), "delete");
        }

        public static async Task DeleteItemRateAsync(int userId, int itemId)
        {
            await eventClient.SetActionItemAsync(userId.ToString(), itemId.ToString(), "delete");
        }

        public static List<ItemRecommendation> GetRecommendation(int userId, int maxCount = 10)
        {
            return engineClient.GetItemRecommendations(userId.ToString(), maxCount);
        }

        public static async Task<List<ItemRecommendation>> GetRecommendationAsync(int userId, int maxCount = 10)
        {
            return await engineClient.GetItemRecommendationsAsync(userId.ToString(), maxCount);
        }

        public static async Task<List<ItemRecommendation>> GetRankingsAsync(int userId, params int[] itemIds)
        {
            return await engineClient.GetItemRankingsAsync(userId.ToString(), itemIds.Select(i => i.ToString()).ToArray());
        }
    }
}
