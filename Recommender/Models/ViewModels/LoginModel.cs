using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Не указан UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        //[DataType(DataType.Password)]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";

    }
}
