using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspCoreApp.Web.Filters;
using MyAspCoreApp.Web.Models;
using MyAspCoreApp.Web.ViewModels;
using System.Diagnostics;
using static MyAspCoreApp.Web.ViewModels.ProductListPartialViewModel;

namespace MyAspCoreApp.Web.Controllers
{
    [LogFilter]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext, IMapper mapper)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [CustomResultFilter(name: "x-version", value: "1.0")]
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            var products = _appDbContext.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel() { Id = x.Id, Name = x.Name, Price = x.Price, Stock = x.Stock }).ToList();
            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };
            return View();
        }
        [CustomExceptionFilter]
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
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(errorViewModel);
        }

        public IActionResult Visitor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {

                var visitor = _mapper.Map<Visitor>(visitorViewModel);
                visitor.Created = DateTime.Now;
                _appDbContext.Visitors.Add(visitor);
                _appDbContext.SaveChanges();

                TempData["result"] = "Yorum Kaydedilmiştir.";

                return RedirectToAction("Visitor");
            }
            catch (Exception)
            {
                TempData["result"] = "Yorum kaydedilirken bir hata meydana geldi.";
                return RedirectToAction("Visitor");
            }

        }
    }
}