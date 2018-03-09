using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class UserRate
    {

        public UserRate() { }

        [JsonProperty("id")]
        [Key, Column("id")]
        public int Id { get; set; }

        [JsonProperty("user_id")]
        [Column("user_id"), ForeignKey("Users")]
        public int UserId { get; set; }

        [JsonProperty("title_id")]
        [Column("title_id")]
        public int TitleId { get; set; }

        [JsonProperty("score")]
        [Column("score")]
        public int Score { get; set; }

        [JsonProperty("review")]
        [Column("review")]
        public string Review { get; set; }

    }
}
