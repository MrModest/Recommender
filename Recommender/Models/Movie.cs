using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class Movie
    {

        public Movie() { }

        public Movie(TMDbLib.Objects.Movies.Movie movieTMDb)
        {
            ProductionCompanies = movieTMDb.ProductionCompanies.Select(p => p.Id).ToArray();
            ProductionCountries = movieTMDb.ProductionCountries.Select(p => p.Iso_3166_1).ToArray();
            ReleaseDate = movieTMDb.ReleaseDate;
            Revenue = movieTMDb.Revenue;
            PosterPath = movieTMDb.PosterPath;
            Runtime = movieTMDb.Runtime;
            SpokenLanguages = movieTMDb.SpokenLanguages.Select(sl => sl.Iso_639_1).ToArray();
            Status = movieTMDb.Status;
            Tagline = movieTMDb.Tagline;
            Title = movieTMDb.Title;
            VoteAverage = movieTMDb.VoteAverage;
            Popularity = movieTMDb.Popularity;
            OriginalTitle = movieTMDb.OriginalTitle;
            Adult = movieTMDb.Adult;
            BackdropPath = movieTMDb.BackdropPath;
            Budget = movieTMDb.Budget;
            Overview = movieTMDb.Overview;
            Homepage = movieTMDb.Homepage;
            Id = movieTMDb.Id;
            ImdbId = movieTMDb.ImdbId;
            OriginalLanguage = movieTMDb.OriginalLanguage;
            Genres = movieTMDb.Genres.Select(g => g.Id).ToArray();
            VoteCount = movieTMDb.VoteCount;
        }

        [JsonProperty("production_companies_ids")]
        [Column("production_companies_ids", TypeName = "integer[]")]
        public int[] ProductionCompanies { get; set; }

        [JsonProperty("production_countries_iso_3166_1")]
        [Column("production_countries_iso_3166_1", TypeName = "text[]")]
        public string[] ProductionCountries { get; set; }

        [JsonProperty("release_date")]
        [Column("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonProperty("revenue")]
        [Column("revenue")]
        public long Revenue { get; set; }

        [JsonProperty("poster_path")]
        [Column("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("runtime")]
        [Column("runtime")]
        public int? Runtime { get; set; }

        [JsonProperty("spoken_languages_iso_639_1")]
        [Column("spoken_languages_iso_639_1", TypeName = "text[]")]
        public string[] SpokenLanguages { get; set; }

        [JsonProperty("status")]
        [Column("status")]
        public string Status { get; set; }

        [JsonProperty("tagline")]
        [Column("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("title")]
        [Column("title")]
        public string Title { get; set; }

        [JsonProperty("vote_average")]
        [Column("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("popularity")]
        [Column("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("original_title")]
        [Column("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("adult")]
        [Column("adult")]
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        [Column("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("budget")]
        [Column("budget")]
        public long Budget { get; set; }

        [JsonProperty("overview")]
        [Column("overview")]
        public string Overview { get; set; }

        [JsonProperty("homepage")]
        [Column("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("id")]
        [Key, Column("id")]
        public int Id { get; set; }

        [JsonProperty("imdb_id")]
        [Column("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("original_language")]
        [Column("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("genres_ids")]
        [Column("genres_ids", TypeName = "integer[]")]
        public int[] Genres { get; set; }

        [JsonProperty("vote_count")]
        [Column("vote_count")]
        public int VoteCount { get; set; }

    }
}
