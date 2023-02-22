using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspCoreApp.Web.Models;

namespace MyAspCoreApp.Web.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var error = context.Exception.Message;

            context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { Errors = new List<string>() { $"{error}" } });
        }
    }
}
