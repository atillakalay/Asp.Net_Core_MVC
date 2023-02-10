using Microsoft.AspNetCore.Mvc;

namespace MyAspCoreApp.Web.Views.Shared.ViewComponents
{
    public class VisitorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
