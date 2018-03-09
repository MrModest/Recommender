using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class ProductionCountry
    {
        public ProductionCountry() { }

        public ProductionCountry(TMDbLib.Objects.Movies.ProductionCountry productionCountry)
        {
            if (productionCountry == null) { return; }

            Iso_3166_1 = productionCountry.Iso_3166_1;
            Name = productionCountry.Name;
        }

        [JsonProperty("iso_3166_1")]
        [Key, Column("iso_3166_1")]
        public string Iso_3166_1 { get; set; }

        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }

    }
}
