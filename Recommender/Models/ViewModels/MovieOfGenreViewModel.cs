using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class MovieOfGenreViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public Genre Genre { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
