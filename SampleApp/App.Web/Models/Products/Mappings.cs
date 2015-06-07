using App.Core.Products;
using AutoMapper;

namespace App.Web.Models.Products
{
    public static class Mappings
    {
        static Mappings()
        {
            Mapper.CreateMap<ProductDto, ProductViewModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(s => string.Format("{0:C}", s.Price)));
            Mapper.CreateMap<ProductDto, EditProductViewModel>();

            Mapper.AssertConfigurationIsValid();
        }

        public static ProductViewModel ToViewModel(this ProductDto product)
        {
            return Mapper.Map<ProductViewModel>(product);
        }

        public static EditProductViewModel ToEditViewModel(this ProductDto product)
        {
            return Mapper.Map<EditProductViewModel>(product);
        }
    }
}