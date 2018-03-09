using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class EFMovieRepository : IMovieRepository
    {

        private TMDbContext context;

        public EFMovieRepository(TMDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Movie> Movies => context.Movies;

    }
}
