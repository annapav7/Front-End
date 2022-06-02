using AutoMapper;
using Catalyte.Apparel.Data.Models;
using Catalyte.Apparel.DTOs;
using Catalyte.Apparel.DTOs.Products;
using Catalyte.Apparel.DTOs.Promos;
using Catalyte.Apparel.DTOs.Purchases;

namespace Catalyte.Apparel.API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<PurchaseRequestDTO, Purchase>();

            CreateMap<Purchase, PurchaseResponseDTO>();
            CreateMap<Purchase, DeliveryAddressDTO>().ReverseMap();
            CreateMap<Purchase, CreditCardDTO>().ReverseMap();
            CreateMap<Purchase, BillingAddressDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.BillingEmail))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.BillingPhone))
                .ReverseMap();
            
            CreateMap<LineItem, LineItemDTO>().ReverseMap();
            
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Promo, PromoDTO>().ReverseMap();
        }

    }
}
