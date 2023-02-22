using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAspCoreApp.Web.Filters
{
    public class CustomResultFilter : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public CustomResultFilter(string value, string name)
        {
            _value = value;
            _name = name;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, _value);
            base.OnResultExecuting(context);
        }
    }
}
