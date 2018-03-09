﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Recommender.Models;
using System;

namespace Recommender.Migrations
{
    [DbContext(typeof(TMDbContext))]
    [Migration("20180226204508_Users")]
    partial class Users
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Recommender.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Headquarters")
                        .HasColumnName("headquarters");

                    b.Property<string>("Homepage")
                        .HasColumnName("homepage");

                    b.Property<string>("LogoPath")
                        .HasColumnName("logo_path");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int>("ParentCompanyId")
                        .HasColumnName("parent_company_id");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Recommender.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("RuName")
                        .HasColumnName("ru_name");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Recommender.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("Adult")
                        .HasColumnName("adult");

                    b.Property<string>("BackdropPath")
                        .HasColumnName("backdrop_path");

                    b.Property<long>("Budget")
                        .HasColumnName("budget");

                    b.Property<int[]>("Genres")
                        .HasColumnName("genres_ids")
                        .HasColumnType("integer[]");

                    b.Property<string>("Homepage")
                        .HasColumnName("homepage");

                    b.Property<string>("ImdbId")
                        .HasColumnName("imdb_id");

                    b.Property<string>("OriginalLanguage")
                        .HasColumnName("original_language");

                    b.Property<string>("OriginalTitle")
                        .HasColumnName("original_title");

                    b.Property<string>("Overview")
                        .HasColumnName("overview");

                    b.Property<double>("Popularity")
                        .HasColumnName("popularity");

                    b.Property<string>("PosterPath")
                        .HasColumnName("poster_path");

                    b.Property<int[]>("ProductionCompanies")
                        .HasColumnName("production_companies_ids")
                        .HasColumnType("integer[]");

                    b.Property<string[]>("ProductionCountries")
                        .HasColumnName("production_countries_iso_3166_1")
                        .HasColumnType("text[]");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnName("release_date");

                    b.Property<long>("Revenue")
                        .HasColumnName("revenue");

                    b.Property<int?>("Runtime")
                        .HasColumnName("runtime");

                    b.Property<string[]>("SpokenLanguages")
                        .HasColumnName("spoken_languages_iso_639_1")
                        .HasColumnType("text[]");

                    b.Property<string>("Status")
                        .HasColumnName("status");

                    b.Property<string>("Tagline")
                        .HasColumnName("tagline");

                    b.Property<string>("Title")
                        .HasColumnName("title");

                    b.Property<double>("VoteAverage")
                        .HasColumnName("vote_average");

                    b.Property<int>("VoteCount")
                        .HasColumnName("vote_count");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Recommender.Models.ProductionCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("ProductionCompanies");
                });

            modelBuilder.Entity("Recommender.Models.ProductionCountry", b =>
                {
                    b.Property<string>("Iso_3166_1")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("iso_3166_1");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Iso_3166_1");

                    b.ToTable("ProductionCountries");
                });

            modelBuilder.Entity("Recommender.Models.SpokenLanguage", b =>
                {
                    b.Property<string>("Iso_639_1")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("iso_639_1");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Iso_639_1");

                    b.ToTable("SpokenLanguages");
                });

            modelBuilder.Entity("Recommender.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.Property<string>("Login")
                        .HasColumnName("login");

                    b.Property<string>("PasswordMD5")
                        .HasColumnName("password_MD5");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Recommender.Models.UserRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Review")
                        .HasColumnName("review");

                    b.Property<int>("Score")
                        .HasColumnName("score");

                    b.Property<int>("TitleId")
                        .HasColumnName("title_id");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("UserRates");
                });
#pragma warning restore 612, 618
        }
    }
}
