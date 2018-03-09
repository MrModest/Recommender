using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Recommender.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(nullable: true),
                    headquarters = table.Column<string>(nullable: true),
                    homepage = table.Column<string>(nullable: true),
                    logo_path = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    parent_company_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    ru_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adult = table.Column<bool>(nullable: false),
                    backdrop_path = table.Column<string>(nullable: true),
                    budget = table.Column<long>(nullable: false),
                    genres_ids = table.Column<int[]>(type: "integer[]", nullable: true),
                    homepage = table.Column<string>(nullable: true),
                    imdb_id = table.Column<string>(nullable: true),
                    original_language = table.Column<string>(nullable: true),
                    original_title = table.Column<string>(nullable: true),
                    overview = table.Column<string>(nullable: true),
                    popularity = table.Column<double>(nullable: false),
                    poster_path = table.Column<string>(nullable: true),
                    production_companies_ids = table.Column<int[]>(type: "integer[]", nullable: true),
                    production_countries_iso_3166_1 = table.Column<string[]>(type: "text[]", nullable: true),
                    release_date = table.Column<DateTime>(nullable: true),
                    revenue = table.Column<long>(nullable: false),
                    runtime = table.Column<int>(nullable: true),
                    spoken_languages_iso_639_1 = table.Column<string[]>(type: "text[]", nullable: true),
                    status = table.Column<string>(nullable: true),
                    tagline = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    vote_average = table.Column<double>(nullable: false),
                    vote_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCompanies",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCompanies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCountries",
                columns: table => new
                {
                    iso_3166_1 = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCountries", x => x.iso_3166_1);
                });

            migrationBuilder.CreateTable(
                name: "SpokenLanguages",
                columns: table => new
                {
                    iso_639_1 = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpokenLanguages", x => x.iso_639_1);
                });

            migrationBuilder.CreateTable(
                name: "UserRates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    review = table.Column<string>(nullable: true),
                    score = table.Column<int>(nullable: false),
                    title_id = table.Column<int>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    email = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    password_MD5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "ProductionCompanies");

            migrationBuilder.DropTable(
                name: "ProductionCountries");

            migrationBuilder.DropTable(
                name: "SpokenLanguages");

            migrationBuilder.DropTable(
                name: "UserRates");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
