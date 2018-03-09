using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class SpokenLanguage
    {
        public SpokenLanguage() { }

        public SpokenLanguage(TMDbLib.Objects.Movies.SpokenLanguage spokenLanguage)
        {
            if (spokenLanguage == null) { return; }

            Iso_639_1 = spokenLanguage.Iso_639_1;
            Name = spokenLanguage.Name;
        }

        [JsonProperty("iso_639_1")]
        [Key, Column("iso_639_1")]
        public string Iso_639_1 { get; set; }

        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }

    }
}
