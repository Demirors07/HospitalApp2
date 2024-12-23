using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HospitalApp.TagHelpers
{
    public class AnchTagHelper : TagHelper
    {
         public int FontWeight {get; set; } = 1000;

          public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("style", "text-decoration: none; font-size: 20px"); // Style is added to the label. (No line is added on the link, font size is set to 20px.)
            output.Attributes.SetAttribute("class", "fw-bold fst-italic");  // CSS classes are added to the tag. (In this example, the classes 'fw-bold' and 'fst-italic' are added.)
        }

    }
}