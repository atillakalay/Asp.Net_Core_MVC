﻿using Microsoft.AspNetCore.Mvc;
using MyAspCoreApp.Web.Models;

namespace MyAspCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _productRepository = new ProductRepository();

            if (!_dbContext.Products.Any())
            {
                _dbContext.Add(new Product() { Name = "Kalem 1", Price = 12, Stock = 85 });
                _dbContext.Add(new Product() { Name = "Kalem 2", Price = 12, Stock = 85 });
                _dbContext.Add(new Product() { Name = "Kalem 3", Price = 12, Stock = 85 });
                _dbContext.Add(new Product() { Name = "Kalem 4", Price = 12, Stock = 85 });
                _dbContext.SaveChanges();
            }
        }

        public IActionResult Index()
        {
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
            return View();
        }
        public IActionResult Update(int id)
        {
            return View();
        }
    }
}
