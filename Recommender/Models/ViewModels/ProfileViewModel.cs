using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<UserMovie> UserMovies { get; set; }
    }
}
