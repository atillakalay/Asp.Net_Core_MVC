using Microsoft.AspNetCore.Mvc;

namespace MyAspCoreApp.Web.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult CookieCreate()
        {
            HttpContext.Response.Cookies.Append("backgorund-color", "red", new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(2)
            });
            return View();
        }

        public IActionResult CookieRead()
        {
            var bgColor = HttpContext.Request.Cookies["backgorund-color"];
            ViewBag.bgColor = bgColor;
            return View();
        }
    }
}
