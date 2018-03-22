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

        public string RateAction { get; set; }

        public string RateController { get; set; }

        public int TitleId { get; set; }

        public int Score { get; set; }

        public bool Default { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            output.TagName = "div";
            output.Attributes.Add("class",         "block-stars");
            output.Attributes.Add("data-rate_url", urlHelper.Action(RateAction, RateController, null));
            output.Attributes.Add("data-title_id", TitleId);
            output.Attributes.Add("data-score",    Score);
            output.Attributes.Add("data-default",  Default.ToString().ToLower());

            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("w3l-ratings");

            TagBuilder li;
            for (int i = 1; i <= 10; i += 2)
            {
                li = new TagBuilder("li");
                li.InnerHtml.AppendHtml(getStarHalf("left",  i));
                li.InnerHtml.AppendHtml(getStarHalf("right", i + 1));

                ul.InnerHtml.AppendHtml(li);
            }

            output.Content.AppendHtml(ul);

        }

        private TagBuilder getStarHalf(string direction, int id)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("star-half");
            div.Attributes.Add("data-star_half_id", id.ToString());

            TagBuilder svg = new TagBuilder("svg");
            svg.Attributes.Add("version",     "1.1");
            svg.Attributes.Add("id",          "Layer_1");
            svg.Attributes.Add("xmlns",       "http://www.w3.org/2000/svg");
            svg.Attributes.Add("xmlns:xlink", "http://www.w3.org/1999/xlink");
            svg.Attributes.Add("x",           "0px");
            svg.Attributes.Add("y",           "0px");
            svg.Attributes.Add("viewBox",     "0 0 8.7 16.7");
            svg.Attributes.Add("xml:space",   "preserve");

            TagBuilder path = new TagBuilder("path");
            path.TagRenderMode = TagRenderMode.SelfClosing;
            path.AddCssClass("star-half-" + direction);
            path.Attributes.Add("d", "M8.9,1.7L6,6.1L0.5,6.9l3.9,3.8l-0.9,5.5c1.8-1,3.6-1.9,5.4-2.9");

            svg.InnerHtml.AppendHtml(path);
            div.InnerHtml.AppendHtml(svg);

            return div;
        }
    }
}
