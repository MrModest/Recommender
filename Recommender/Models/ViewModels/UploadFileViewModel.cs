using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class UploadFileViewModel
    {
        public IFormFile File { get; set; }
        public string Type { get; set; }
    }
}
