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
            var mappedProductList = _mapper.Map<List<ProductViewModel>>(products);
            return View(mappedProductList);
        }

        public IActionResult Pages(int page, int pageSize)
        {
            var products = _dbContext.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        public IActionResult GetById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            return View(_mapper.Map<ProductViewModel>(product));
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
        public IActionResult Add(ProductViewModel productViewModel)
        {
            //1. Yöntem
            //var name = HttpContext.Request.Form["name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"]);
            //var stock = int.Parse(HttpContext.Request.Form["Stock"]);
            //var color = HttpContext.Request.Form["Color"];
            //Product product = new Product() { Color = Color, Name = Name, Price = Price, Stock = Stock };
            ViewBag.Expire = new Dictionary<string, int>() { { "1 Ay", 1 }, { "3 Ay", 3 }, { "6 Ay", 6 }, { "12 Ay", 12 } };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data = "Mavi",Value = "Mavi"},
                new(){Data = "Kırmızı",Value = "Kırmızı"},
                new(){Data = "Beyaz",Value = "Beyaz"},
                new(){Data = "Siyah",Value = "Siyah"}
            }, "Value", "Data");



            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Products.Add(_mapper.Map<Product>(productViewModel));
                    _dbContext.SaveChanges();
                    TempData["status"] = "Ürün başarıyla eklendi";
                    return RedirectToAction("Add");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ürün kaydedilirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.");
                    return View();
                }
            }
            else
            {
                return View();
            }

            return RedirectToAction("Add");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _dbContext.Products.Find(id);
            var mappedProduct = _mapper.Map<ProductViewModel>(product);

            ViewBag.ExpireValue = Convert.ToInt32(mappedProduct.Expire);

            ViewBag.Expire = new Dictionary<string, int>() { { "1 Ay", 1 }, { "3 Ay", 3 }, { "6 Ay", 6 }, { "12 Ay", 12 } };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new(){Data = "Mavi",Value = "Mavi"},
                new(){Data = "Kırmızı",Value = "Kırmızı"},
                new(){Data = "Beyaz",Value = "Beyaz"},
                new(){Data = "Siyah",Value = "Siyah"}
            }, "Value", "Data", mappedProduct.Color);

            return View(mappedProduct);
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel updateProduct)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ExpireValue = Convert.ToInt32(updateProduct.Expire);

                ViewBag.Expire = new Dictionary<string, int>() { { "1 Ay", 1 }, { "3 Ay", 3 }, { "6 Ay", 6 }, { "12 Ay", 12 } };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
                {
                    new(){Data = "Mavi",Value = "Mavi"},
                    new(){Data = "Kırmızı",Value = "Kırmızı"},
                    new(){Data = "Beyaz",Value = "Beyaz"},
                    new(){Data = "Siyah",Value = "Siyah"}
                }, "Value", "Data", updateProduct.Color);
            }

            _dbContext.Products.Update(_mapper.Map<Product>(updateProduct));
            _dbContext.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi";

            return RedirectToAction("Index");
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult HasProductName(string name)
        {
            var anyProduct = _dbContext.Products.Any(x => x.Name.ToLower() == name.ToLower());

            if (anyProduct)
            {
                return Json("Kaydetmeye çalıştığınız ürün ismi veritabanında bulunmaktadır.");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
