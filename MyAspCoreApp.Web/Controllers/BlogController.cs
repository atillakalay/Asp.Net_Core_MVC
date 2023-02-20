using Microsoft.AspNetCore.Mvc;

namespace MyAspCoreApp.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Article(string name, int id)
        {
            //var routes = Request.RouteValues["article"];
            return View();
        }



    }
}
