using AutoMapper;
using BL.DTO;
using Core.Models;

namespace BL.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Cart, CartDTO>().ReverseMap();
    }
}