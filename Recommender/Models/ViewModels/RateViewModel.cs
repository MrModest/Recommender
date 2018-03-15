using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class RateViewModel
    {
        [JsonProperty("title_id")]
        public int TitleId { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    }
}
