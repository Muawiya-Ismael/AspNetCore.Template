using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MvcTemplate.Components.Mvc
{
    [HtmlTargetElement("select", Attributes = "asp-for")]
    public class FormSelectTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression? For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes["class"] == null)
                output.Attributes.Insert(0, new TagHelperAttribute("class", "form-control"));
            else
                output.Attributes.SetAttribute("class", $"form-control {output.Attributes["class"].Value}");

            if (!For!.Metadata.IsRequired)
                output.PreContent.AppendHtml(new TagBuilder("option"));
        }
    }
}
