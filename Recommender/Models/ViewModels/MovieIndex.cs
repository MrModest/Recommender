using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class MovieIndex
    {
        public IEnumerable<UserMovie> MoviesOnCinema { get; set; }
        public IEnumerable<UserMovie> RecommendedMovies { get; set; }
        public IEnumerable<UserMovie> InterestingMovies { get; set; }
    }
}
