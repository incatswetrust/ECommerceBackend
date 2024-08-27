using ECommerceBackend.Middleware;

namespace ECommerceBackend;

using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping between Product and ProductDto
        CreateMap<Product, ProductDto>().ReverseMap();

        // Mapping between User and UserDto
        CreateMap<User, UserDto>().ReverseMap();

        // Mapping between Order and OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        // Mapping between OrderItem and OrderItemDto
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ReverseMap();
    }
}
