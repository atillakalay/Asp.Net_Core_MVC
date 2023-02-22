using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace MyAspCoreApp.Web.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Action Method Çalışmadan Önce");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Debug.WriteLine("Action Method Çalıştıktan Sonra");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("Action Method sonuç üretilmeden Önce");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action Method sonuç üretildikten sonra.");
        }
    }
}
