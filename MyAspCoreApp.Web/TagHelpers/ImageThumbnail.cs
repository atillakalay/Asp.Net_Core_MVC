using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspCoreApp.Web.TagHelpers
{
    [HtmlTargetElement("thumbnail")]
    public class ImageThumbnail : TagHelper
    {
        public string ImageSrc { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            string fileName = ImageSrc.Split(".")[0];
            string fileExtension = Path.GetExtension(ImageSrc);

            output.Attributes.SetAttribute("src", $"{fileName}-100X100{fileExtension}");
        }
    }
}
