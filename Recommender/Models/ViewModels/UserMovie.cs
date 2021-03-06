﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class UserMovie
    {
        public Movie Movie { get; set; }
        public int UserScore { get; set; }
        public double PredictedScore { get; set; }
        public bool NotRated { get { return UserScore == 0; } }

        public UserMovie() { }

        public UserMovie(Movie movie)
        {
            Movie = movie;
        }

        public UserMovie(Movie movie, int userScore)
        {
            Movie = movie;
            UserScore = userScore;
        }
    }
}
