using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCoreScriptTagHelperOverride
{
    [HtmlTargetElement("script")] // A
    public class MyScriptTagHelper : ScriptTagHelper
    {
        public MyScriptTagHelper(
            IHostingEnvironment hostingEnvironment,
            IMemoryCache cache,
            HtmlEncoder htmlEncoder,
            JavaScriptEncoder javaScriptEncoder,
            IUrlHelperFactory urlHelperFactory) : base(
                hostingEnvironment,
                cache,
                htmlEncoder,
                javaScriptEncoder,
                urlHelperFactory)
        { } // B

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            const string appendVersion = "asp-append-version";
            if (!context.AllAttributes.Any(a => a.Name == appendVersion))
            {
                var attributes = new TagHelperAttributeList(context.AllAttributes);
                attributes.Add(appendVersion, true);
                context = new TagHelperContext(attributes, context.Items, context.UniqueId);
            } // E

            base.AppendVersion = true; // C
            base.Process(context, output); // D
        }
    }
}