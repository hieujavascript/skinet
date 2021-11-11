using API.Dto;
using AutoMapper;
using Core.Entity;
using Core.Entity.Identity;

namespace API.helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product , ProductToReturnDto>()
            .ForMember(dto => dto.ProductBrand  , p => p.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dto => dto.ProductType  , p => p.MapFrom(src => src.ProductType.Name))
            // Resolver gia trong destinationMember thành giá trị khác , thêm localhost vào trước URL Image
            .ForMember(d => d.PictureUrl , p => p.MapFrom<ProductUrlResolver>()); 
            CreateMap<Address , AddressDto>();
            CreateMap<AddressDto,Address>();
            CreateMap<BasketItemDto , BasketItem>();
            CreateMap<CustomerBasketDto , CustomerBasket>();
        }
    }
}