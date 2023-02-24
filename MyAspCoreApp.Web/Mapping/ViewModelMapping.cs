using AutoMapper;
using MyAspCoreApp.Web.Models;
using MyAspCoreApp.Web.ViewModels;

namespace MyAspCoreApp.Web.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<ProductUpdateViewModel, Product>().ReverseMap();
            CreateMap<VisitorViewModel, Visitor>().ReverseMap();
        }
    }
}
