using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspCoreApp.Web.Filters;
using MyAspCoreApp.Web.Models;
using MyAspCoreApp.Web.ViewModels;

namespace MyAspCoreApp.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _dbContext;
        private readonly IFileProvider _fileProvider;

        public ProductsController(AppDbContext dbContext, IMapper mapper, IFileProvider fileProvider)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileProvider = fileProvider;
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

        //[CacheResourceFilter]
        public IActionResult Index()
        {
            List<ProductViewModel> products = _dbContext.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Stock = x.Stock,
                CategoryName = x.Category.Name,
                Color = x.Color,
                Expire = x.Expire,
                ImagePath = x.ImagePath,
                IsPublished = x.IsPublished,
                PublishDate = x.PublishDate
            }).ToList();
            return View(products);
        }
        [Route("[controller]/[action]/{page}/{pageSize}", Name = "productPage")]
        public IActionResult Pages(int page, int pageSize)
        {
            var products = _dbContext.Products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [Route("[controller]/[action]/{id}", Name = "product")]
        public IActionResult GetById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            return View(_mapper.Map<ProductViewModel>(product));
        }
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
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

            var categories = _dbContext.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

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

            IActionResult result = ViewBag.Expire = new Dictionary<string, int>() { { "1 Ay", 1 }, { "3 Ay", 3 }, { "6 Ay", 6 }, { "12 Ay", 12 } };

            var categories = _dbContext.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

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
                    var product = _mapper.Map<Product>(productViewModel);
                    if (productViewModel.Image != null && productViewModel.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(productViewModel.Image.FileName);


                        var path = Path.Combine(images.PhysicalPath, randomImageName);
                        using var stream = new FileStream(path, FileMode.Create);
                        productViewModel.Image.CopyTo(stream);

                        product.ImagePath = randomImageName;
                    }


                    _dbContext.Products.Add(product);
                    _dbContext.SaveChanges();
                    TempData["status"] = "Ürün başarıyla eklendi";
                    return RedirectToAction("Add");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Ürün kaydedilirken bir hata meydana geldi. Lütfen daha sonra tekrar deneyiniz.");

                    result = View();
                }
            }
            else
            {
                result = View();
            }

            return RedirectToAction("Add");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _dbContext.Products.Find(id);
            var mappedProduct = _mapper.Map<ProductViewModel>(product);

            var categories = _dbContext.Category.ToList();
            ViewBag.categorySelect = new SelectList(categories, "Id", "Name", product.CategoryId);

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
        public IActionResult Update(ProductUpdateViewModel updateProduct)
        {
            if (ModelState.IsValid)
            {
                var categories = _dbContext.Category.ToList();
                ViewBag.categorySelect = new SelectList(categories, "Id", "Name", updateProduct.CategoryId);

                ViewBag.ExpireValue = Convert.ToInt32(updateProduct.Expire);

                ViewBag.Expire = new Dictionary<string, int>() { { "1 Ay", 1 }, { "3 Ay", 3 }, { "6 Ay", 6 }, { "12 Ay", 12 } };

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
                {
                    new(){Data = "Mavi",Value = "Mavi"},
                    new(){Data = "Kırmızı",Value = "Kırmızı"},
                    new(){Data = "Beyaz",Value = "Beyaz"},
                    new(){Data = "Siyah",Value = "Siyah"}
                }, "Value", "Data", updateProduct.Color);
                if (updateProduct.Image != null && updateProduct.Image.Length > 0)
                {
                    var root = _fileProvider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "images");

                    var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);


                    var path = Path.Combine(images.PhysicalPath, randomImageName);
                    using var stream = new FileStream(path, FileMode.Create);
                    updateProduct.Image.CopyTo(stream);

                    updateProduct.ImagePath = randomImageName;
                }
            }

            var product = _mapper.Map<Product>(updateProduct);
            _dbContext.Products.Update(product);
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
