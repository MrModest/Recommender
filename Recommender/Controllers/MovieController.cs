using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recommender.Models;
using Recommender.Models.ViewModels;

namespace Recommender.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository movieRep;
        private IGenreRepository genreRep;

        public MovieController(IMovieRepository movieRep, IGenreRepository genreRep)
        {
            this.movieRep = movieRep;
            this.genreRep = genreRep;
        }

        public IActionResult Index()
        {
            return View(movieRep.Movies
                                    .Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year > (DateTime.Now.Year - 2))
                                    .Where(m => m.VoteCount > 100)
                                    .OrderByDescending(m => m.VoteAverage)
                                    .Take(10));

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

        public ViewResult List() => 
            View(movieRep.Movies
                             .Where(m => m.PosterPath != null && m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == DateTime.Now.Year)
                             .OrderByDescending(m => m.Popularity)
                             .Take(10));

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

            var movies = await movieRep.Movies
                .Where(m => m.Genres.Contains(genreId))
                .OrderByDescending(m => m.Popularity)
                .Skip((page - 1) * pagingInfo.ItemsPerPage)
                .Take(pagingInfo.ItemsPerPage)
                .ToListAsync();

            if (movies == null || movies.Count == 0) { return NotFound(); }
            return View(new MovieOfGenreViewModel {
                Genre = genre,
                Movies = movies,
                PagingInfo = pagingInfo
            });
        }
    }
}