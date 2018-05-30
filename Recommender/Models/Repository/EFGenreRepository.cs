using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class EFGenreRepository : IGenreRepository
    {

        private TMDbContext context;

        public EFGenreRepository(TMDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Genre> Genres => context.Genres;
    }
}
