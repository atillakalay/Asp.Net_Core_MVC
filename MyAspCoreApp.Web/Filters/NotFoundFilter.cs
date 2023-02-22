using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspCoreApp.Web.Models;

namespace MyAspCoreApp.Web.Filters
{
    public class NotFoundFilter : ActionFilterAttribute
    {
        private readonly AppDbContext _context;

        public NotFoundFilter(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idValue = context.ActionArguments.Values.First();
            var id = (int)idValue;

            var hasProducts = _context.Products.Any(x => x.Id == id);
            if (hasProducts == false)
            {
                context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { Errors = new List<string>() { $"Id({id})' ye sahip ürün bulunamamıştır." } });
            }
        }
    }
}
