using Microsoft.AspNetCore.Mvc;

namespace MyAspCoreApp.Web.Controllers

{

    //public class Product
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            //***Viewbag ve ViewData ile action methodun cshtml sayfasına veri taşırken, tempdata ile sayfalar arası veri taşırız.

            //ViewBag.name = "Asp.Net Core";
            //ViewBag.person = new { id = 1, name = "Atilla", age = 23 };

            //ViewData["age"] = 23;
            //ViewData["names"] = new List<string> { "Atilla", "Yasin", "Veysel", "Salih", "İbrahim" };

            //TempData["surname"] = "Kalay";

            //var productList = new List<Product>
            //{
            //    new Product{Id = 1,Name = "Kalem"},
            //    new Product{Id = 2,Name = "Defter"},
            //    new Product{Id = 3,Name = "Silgi"}
            //};
            return View();
        }

        public IActionResult Index2()
        {

            //return RedirectToAction("Index", "Example");
            return View();
        }

        public IActionResult ParametreView(int id)
        {
            return RedirectToAction("JsonResultParametre", "Example", new { id = id });
        }

        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id = id });
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

        public IActionResult LayoutExample()
        {
            return View();
        }

        public IActionResult NoLayout()
        {
            return View();
        }
    }
}
