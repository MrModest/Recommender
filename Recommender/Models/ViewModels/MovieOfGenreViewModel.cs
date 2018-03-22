using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class MovieOfGenreViewModel
    {
        public IEnumerable<UserMovie> UserMovies { get; set; }
        public Genre Genre { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
