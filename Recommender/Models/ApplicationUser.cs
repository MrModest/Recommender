﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string AvatarImagePath { get; set; }
        public string BackgroundImagePath { get; set; }
    }
}
