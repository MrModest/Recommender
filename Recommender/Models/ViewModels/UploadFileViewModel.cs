using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recommender.Models.ViewModels
{
    public class UploadFileViewModel
    {
        [JsonProperty("file")]
        public IFormFile File { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
