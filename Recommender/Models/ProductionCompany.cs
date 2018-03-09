using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class ProductionCompany
    {
        public ProductionCompany() { }

        public ProductionCompany(TMDbLib.Objects.Movies.ProductionCompany productionCompany)
        {
            if (productionCompany == null) { return; }

            Id = productionCompany.Id;
            Name = productionCompany.Name;
        }

        [JsonProperty("id")]
        [Key, Column("id")]
        public int Id { get; set; }


        [JsonProperty("name")]
        [Column("name")]
        public string Name { get; set; }

    }
}
