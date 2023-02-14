using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspCoreApp.Web.Models;
using MyAspCoreApp.Web.ViewModels;

namespace MyAspCoreApp.Web.Views.Shared.ViewComponents
{
    //[ViewComponent(Name = "p-list")]
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductListViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int type = 1)
        {
            var viewModels = await _context.Products.Select(x => new ProductListComponentViewModel() { Description = x.Description, Name = x.Name }).ToListAsync();

            if (type == 1)
            {
                return View("Default", viewModels);
            }
            else
            {
                return View("Type2", viewModels);
            }

        }
    }
}
