using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recommender.Models;
using Recommender.Models.ViewModels;
using Recommender.Infrastructure;

namespace Recommender.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository    movieRep;
        private IGenreRepository    genreRep;
        private IUserRateRepository userRateRep;

        public MovieController(IMovieRepository movieRep, IGenreRepository genreRep, IUserRateRepository userRateRep)
        {
            this.movieRep = movieRep;
            this.genreRep = genreRep;
            this.userRateRep = userRateRep;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<UserRate> userRates = await User.GetUserRatesAsync(userRateRep);

            IEnumerable<Movie> movies = await movieRep.Movies
                                    .Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year > (DateTime.Now.Year - 2))
                                    .Where(m => m.VoteCount > 100)
                                    .OrderByDescending(m => m.VoteAverage)
                                    .Take(10)
                                    .ToListAsync();

            return View(movies.GetUserMovies(userRates));

            //var movies = repository.Movies
            //                        .Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == DateTime.Now.Year)
            //                        .OrderByDescending(m => m.VoteAverage)
            //                        .Take(10).ToList();
            //for (int i = 0; i < 10; i++)
            //{
            //    movies[i].VoteAverage = i;
            //}

            //return View(movies);

        }

        public async Task<IActionResult> List() => 
            View(await movieRep.Movies
                             .Where(m => m.PosterPath != null && m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == DateTime.Now.Year)
                             .OrderByDescending(m => m.Popularity)
                             .Take(10)
                             .ToListAsync());

        public async Task<IActionResult> Title(int titleId)
        {
            var movie = await movieRep.Movies.FirstOrDefaultAsync(m => m.Id == titleId);
            if (movie == null) { return NotFound(); }
            return View(movie);
        }

        public async Task<IActionResult> Genre(int genreId, int page = 1)
        {
            var genre = await genreRep.Genres.FirstOrDefaultAsync(g => g.Id == genreId);

            if (genre == null) { return NotFound(); }

            var count = await movieRep.Movies.Where(m => m.Genres.Contains(genreId)).CountAsync();

            var pagingInfo = new PagingInfo(count, 24, page);

            var movies = await movieRep.GetMoviesByGenre(genreId)
                .OrderByDescending(m => m.Popularity)
                .Skip((page - 1) * pagingInfo.ItemsPerPage)
                .Take(pagingInfo.ItemsPerPage)
                .ToListAsync();

            if (movies == null) { movies = new List<Movie>(); }

            IEnumerable<UserRate> userRates = await User.GetUserRatesAsync(userRateRep);

            return View(new MovieOfGenreViewModel {
                Genre = genre,
                UserMovies = movies.GetUserMovies(userRates),
                PagingInfo = pagingInfo
            });
        }

        public async Task<IActionResult> Search(string query, int page = 1)
        {
            var moviesByQuery = movieRep.Movies.Where(m => m.Title.Contains(query) || m.OriginalTitle.Contains(query));

            var count = await moviesByQuery.CountAsync();

            var pagingInfo = new PagingInfo(count, 24, page);

            var movies = await moviesByQuery.
                OrderByDescending(m => m.Popularity)
                .Skip((page - 1) * pagingInfo.ItemsPerPage)
                .Take(pagingInfo.ItemsPerPage)
                .ToListAsync();

            if (movies == null) { movies = new List<Movie>(); }

            IEnumerable<UserRate> userRates = await User.GetUserRatesAsync(userRateRep);

            return View(new SearchResult
            {
                Query = query,
                Results = movies,
                PagingInfo = pagingInfo
            });
        }

        public async Task<IActionResult> Test() => View(await movieRep.Movies
                             .Where(m => m.PosterPath != null && m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == DateTime.Now.Year)
                             .OrderByDescending(m => m.Popularity)
                             .Take(10)
                             .ToListAsync());
    }
}