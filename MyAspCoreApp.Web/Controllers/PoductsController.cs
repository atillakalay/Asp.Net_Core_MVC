using Microsoft.AspNetCore.Mvc;
using MyAspCoreApp.Web.Helpers;
using MyAspCoreApp.Web.Models;

namespace MyAspCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _dbContext;
        private readonly IHelper _helper;

        public ProductsController(AppDbContext dbContext, IHelper helper)
        {
            _dbContext = dbContext;
            _helper = helper;
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

        public IActionResult Index([FromServices] IHelper helper)
        {
            var text = "Asp.Net";
            var upperText = _helper.Upper(text);

            var products = _dbContext.Products.ToList();
            return View(products);
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
