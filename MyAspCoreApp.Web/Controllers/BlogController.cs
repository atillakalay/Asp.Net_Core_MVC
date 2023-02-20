using Microsoft.AspNetCore.Mvc;

namespace MyAspCoreApp.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Article()
        {
            var routes = Request.RouteValues["article"];
            return View();
        }

        public IActionResult AritcleSingle(string name)
        {

        }

    }
}
