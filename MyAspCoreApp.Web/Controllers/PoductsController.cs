using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspCoreApp.Web.Models;
using MyAspCoreApp.Web.ViewModels;

namespace MyAspCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _productRepository = new ProductRepository();

            //if (!_dbContext.Products.Any())
            //{
            //    _dbContext.Add(new Product() { Name = "Kalem 1", Price = 12, Stock = 85 });
            //    _dbContext.Add(new Product() { Name = "Kalem 2", Price = 12, Stock = 85 });
            //    _dbContext.Add(new Product() { Name = "Kalem 3", Price = 12, Stock = 85 });
            //    _dbContext.Add(new Product() { Name = "Kalem 4", Price = 12, Stock = 85 });
            //    _dbContext.SaveChanges();
            //}
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        public IActionResult Remove(int id)
        {
            var product = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Expire = new Dictionary<string, int>() { { "1 Ay", 1 }, { "3 Ay", 3 }, { "6 Ay", 6 }, { "12 Ay", 12 } };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data = "Mavi",Value = "Mavi"},
                new(){Data = "Kırmızı",Value = "Kırmızı"},
                new(){Data = "Beyaz",Value = "Beyaz"},
                new(){Data = "Siyah",Value = "Siyah"}
            }, "Value", "Data");

            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            //1. Yöntem
            //var name = HttpContext.Request.Form["name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"]);
            //var stock = int.Parse(HttpContext.Request.Form["Stock"]);
            //var color = HttpContext.Request.Form["Color"];
            //Product product = new Product() { Color = Color, Name = Name, Price = Price, Stock = Stock };
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            TempData["status"] = "Ürün başarıyla eklendi";

            return RedirectToAction("Add");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _dbContext.Products.Find(id);

            ViewBag.ExpireValue = Convert.ToInt32(product.Expire);

            ViewBag.Expire = new Dictionary<string, int>() { { "1 Ay", 1 }, { "3 Ay", 3 }, { "6 Ay", 6 }, { "12 Ay", 12 } };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data = "Mavi",Value = "Mavi"},
                new(){Data = "Kırmızı",Value = "Kırmızı"},
                new(){Data = "Beyaz",Value = "Beyaz"},
                new(){Data = "Siyah",Value = "Siyah"}
            }, "Value", "Data", product.Color);

            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product, int productId)
        {
            product.Id = productId;
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi";

            return RedirectToAction("Index");
        }
    }
}
