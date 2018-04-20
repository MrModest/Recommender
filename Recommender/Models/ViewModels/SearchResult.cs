using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class SearchResult
    {
        public string Query { get; set; }
        public IEnumerable<Movie> Results { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
