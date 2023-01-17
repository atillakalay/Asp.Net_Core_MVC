using Microsoft.AspNetCore.Mvc;

namespace MyAspCoreApp.Web.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {

            return RedirectToAction("Index", "Example");
        }

        public IActionResult ContentResult()
        {
            return Content("ContentResult");
        }

        public IActionResult JsonResult()
        {
            return Json(new { Id = 1, name = "Kalem 1", price = 17.5 });
        }

        public IActionResult EmptyResult()
        {
            return new EmptyResult();
        }
    }
}
