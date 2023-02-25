using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspCoreApp.Web.Models;
using MyAspCoreApp.Web.ViewModels;

namespace MyAspCoreApp.Web.Controllers
{
    public class VisitorAjaxController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public VisitorAjaxController(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {

            var visitor = _mapper.Map<Visitor>(visitorViewModel);
            visitor.Created = DateTime.Now;
            _appDbContext.Visitors.Add(visitor);
            _appDbContext.SaveChanges();

            return Json(new { IsSuccess = true });

        }
        [HttpGet]
        public async Task<IActionResult> VisitorCommentList()
        {
            var visitors = await _appDbContext.Visitors.OrderByDescending(x => x.Created).ToListAsync();
            var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitors);
            return Json(visitorViewModels);
        }
    }


}
