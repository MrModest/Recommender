using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Не указан UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        //public IFormFile Avatar { get; set; }
        //public IFormFile BackgroundImage { get; set; }

        public string AvatarImageUrl { get; set; }
        public string BackgroundImageUrl { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
