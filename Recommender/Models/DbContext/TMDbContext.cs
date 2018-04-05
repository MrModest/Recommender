using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Recommender.Models
{
    public class TMDbContext : DbContext
    {
        public TMDbContext() : base() { }

        public TMDbContext(DbContextOptions<TMDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<ProductionCompany> ProductionCompanies { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ProductionCountry> ProductionCountries { get; set; }

        public DbSet<SpokenLanguage> SpokenLanguages { get; set; }

        public DbSet<UserRate> UserRates { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(@"host=localhost;database=TMDbMovies;user id=postgres;password=predict;");
        //}
    }
}
