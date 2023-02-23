using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAspCoreApp.Web.Filters
{
    public class CacheResourceFilter : Attribute, IResourceFilter
    {
        private static IActionResult _cache;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cache = context.Result;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (_cache != null)
            {
                context.Result = _cache;
            }
        }
    }
}
