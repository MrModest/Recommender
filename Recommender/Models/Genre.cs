using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class Genre
    {
        public Genre() { }

        public Genre(TMDbLib.Objects.General.Genre genreEn, TMDbLib.Objects.General.Genre genreRu)
        {
            if (genreEn == null || genreRu == null) { return; }

            if (genreEn.Id != genreRu.Id)
            {
                throw new Exception("Difference genres!");
            }

            Id = genreEn.Id;
            Name = genreEn.Name;
            RuName = genreRu.Name;
        }

        [JsonProperty("id")]
        [Key, Column("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }

        [JsonProperty("ru_name")]
        [Column("ru_name")]
        public string RuName { get; set; }

    }
}
