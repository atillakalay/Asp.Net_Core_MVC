using Microsoft.AspNetCore.Mvc;
using MyAspCoreApp.Web.Models;

namespace MyAspCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;

            if (!_productRepository.GetAll().Any())
            {
                _productRepository.Add(new Product() { Id = 1, Name = "Kalem", Price = 15, Stock = 750 });
                _productRepository.Add(new Product() { Id = 2, Name = "Silgi", Price = 25, Stock = 500 });
                _productRepository.Add(new Product() { Id = 3, Name = "Kalem Kutusu", Price = 35, Stock = 250 });
            }
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }
    }
}
