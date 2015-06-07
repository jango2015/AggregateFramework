using AutoMapper;

namespace App.Core.Products
{
    public static class Mappings
    {
        static Mappings()
        {
            Mapper.CreateMap<ProductState, ProductDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(s => s.Price / 100.0));

            Mapper.AssertConfigurationIsValid();
        }

        public static ProductDto ToDto(this ProductState state)
        {
            return Mapper.Map<ProductDto>(state);
        }
    }
}
