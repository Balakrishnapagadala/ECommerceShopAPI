using AutoMapper;
using ECommerceShopAPI.Entities.DTO;
using ECommerceShopAPI.Entities.Entities;
using ECommerceShopAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShopAPI.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<Cart, CartEntity>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartEntity, Cart>().ReverseMap();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<ProductDto,ProductEntity>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<PurchaseOrderDto, PurchaseOrderEntity>().ReverseMap();
            CreateMap<PurchaseOrderDto, Order>().ForMember
                (dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<ProductEntity, CartEntity>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<PurchaseOrderEntity, Order>().ForMember
                (dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
        }
    }
}
