using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class ProfileViewModel
    {
        public IdentityUser<int> User { get; set; }
        public IEnumerable<UserRate> UserRates { get; set; }
        public IEnumerable<Movie> UserMovies { get; set; }
    }
}
