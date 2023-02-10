using Microsoft.AspNetCore.Mvc;
using MyAspCoreApp.Web.Models;
using MyAspCoreApp.Web.ViewModels;
using System.Diagnostics;
using static MyAspCoreApp.Web.ViewModels.ProductListPartialViewModel;

namespace MyAspCoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var products = _appDbContext.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel() { Id = x.Id, Name = x.Name, Price = x.Price, Stock = x.Stock }).ToList();
            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };
            return View();
        }

        public IActionResult Privacy()
        {
            var products = _appDbContext.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel() { Id = x.Id, Name = x.Name, Price = x.Price, Stock = x.Stock }).ToList();
            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Visitor()
        {
            return View();
        }
    }
}