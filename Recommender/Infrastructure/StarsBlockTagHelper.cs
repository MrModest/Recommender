using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recommender.Models.ViewModels;
using Recommender.Models;

namespace Recommender.Infrastructure
{
    public class StarsBlockTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public StarsBlockTagHelper(IUrlHelperFactory helperFactory)
        {
            this.urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public Movie PageModel { get; set; }

        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            output.TagName = "div";
            output.Attributes.Add("class", "block-stars");
            output.Attributes.Add("data-score", (PageModel.VoteAverage > 0 && PageModel.VoteCount > 100) ? PageModel.VoteAverage : 0);
            

        }
    }
}
