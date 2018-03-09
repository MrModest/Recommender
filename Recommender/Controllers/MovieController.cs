using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommender.Models;

namespace Recommender.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository repository;

        public MovieController(IMovieRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View(repository.Movies
                                    .Where(m => m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == DateTime.Now.Year)
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
            View(repository.Movies
                             .Where(m => m.PosterPath != null && m.ReleaseDate.HasValue && m.ReleaseDate.Value.Year == DateTime.Now.Year)
                             .OrderByDescending(m => m.Popularity)
                             .Take(10));

        public IActionResult Title(int titleId)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.Id == titleId);
            if (movie == null) { return NotFound(); }
            return View(movie);
        }

    }
}