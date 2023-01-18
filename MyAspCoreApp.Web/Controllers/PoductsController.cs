using Microsoft.AspNetCore.Mvc;
using MyAspCoreApp.Web.Models;

namespace MyAspCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductsController()
        {
            _productRepository = new ProductRepository();
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        public IActionResult Remove(int id)
        {
            _productRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
