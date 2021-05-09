using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MvcTemplate.Components.Mvc
{
    [HtmlTargetElement("input", Attributes = "asp-for")]
    public class FormInputTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression? For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            String classes = For?.Metadata.ModelType == typeof(Boolean) ? "form-check-input" : "form-control";

            if (output.Attributes["autocomplete"] == null)
                output.Attributes.Add("autocomplete", "off");

            if (output.Attributes["class"] == null)
                output.Attributes.Insert(0, new TagHelperAttribute("class", classes));
            else
                output.Attributes.SetAttribute("class", $"{classes} {output.Attributes["class"].Value}");
        }
    }
}
