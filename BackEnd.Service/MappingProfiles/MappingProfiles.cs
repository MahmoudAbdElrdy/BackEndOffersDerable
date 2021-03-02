using AutoMapper;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.Client;
using BackEnd.Service.DTO.Companies;
using BackEnd.Service.DTO.Prodcuts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.Service.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<ApplicationUser, UserRegisteration>().ReverseMap();
            CreateMap<Company, ShowCompanyDto>().ForMember(x=>x.User,m=>m.MapFrom(x=>x.User)).ReverseMap();
            CreateMap<Product, ShowProductDto>().
           ForMember(x => x.CategoryName, x => x.MapFrom(x => x.Category.CategoryName))
          . ForMember(x => x.CompanyName, x => x.MapFrom(x => x.Company.User.FullName)).
          ReverseMap()
          .ForMember(x => x.ProductImages, opt => opt.MapFrom(x => x.ProductImages.Select(x => (string.Concat(getPath(), x)))));

            CreateMap<ProductImages, string>().ConstructUsing(x => x.ProductImage);
            CreateMap<string, ProductImages>().ForMember(dest => dest.ProductImage, m => m.MapFrom(src => src));
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Discount, DiscountDto>()
                         .ReverseMap();
            CreateMap<Discount, ShowDiscountDto>().ReverseMap();
            CreateMap<Discount, ShowListProductDto>()
           .ForMember(dest=>dest.ProductDescription, m=>m.MapFrom(x=>x.Product.Description))
           .ForMember(dest=>dest.OldPrice,m=>m.MapFrom(x=>x.Product.Price))
           .ForMember(dest=>dest.DiscountDescription, m=>m.MapFrom(x=>x.DiscountDescription))
           .ForMember(dest=>dest.ProductId,m=>m.MapFrom(x=>x.Product.Id))
           .ForMember(dest=>dest.ProdcutName,m=>m.MapFrom(x=>x.Product.ProdcutName))
           .ForMember(dest=>dest.ProductImage,m=>m.MapFrom(x=>x.Product.ProductImages.FirstOrDefault().ProductImage))
           .ForMember(dest=>dest.NewPrice,m=>m.MapFrom(x=> (int)x.DiscountType==1?x.Product.Price-x.DiscountValue:(x.Product.Price-(x.Product.Price * (x.DiscountValue / 100)))))
                ;
          CreateMap<Discount,ShowDiscountDto>()
         .ForMember(dest => dest.ProductDescription, m => m.MapFrom(x => x.Product.Description))
         .ForMember(dest => dest.OldPrice, m => m.MapFrom(x => x.Product.Price))
         .ForMember(x => x.CompanyPhoneNumber, x => x.MapFrom(x => x.Product.Company.User.PhoneNumber))
         .ForMember(x => x.CompanyName, x => x.MapFrom(x => x.Product.Company.User.FullName))
         .ForMember(x => x.CompanyLogo, x => x.MapFrom(x => x.Product.Company.User.Image))
         .ForMember(x => x.CompanyId, x => x.MapFrom(x => x.Product.Company.Id))
         .ForMember(x => x.CompanyDescription, x => x.MapFrom(x => x.Product.Company.CompanyDescription))
         .ForMember(dest => dest.DiscountDescription, m => m.MapFrom(x => x.DiscountDescription))
         .ForMember(dest => dest.ProductId, m => m.MapFrom(x => x.Product.Id))
         .ForMember(dest => dest.NumberDays, m => m.MapFrom(x => (x.EndDate.Value.Date-x.SatrtDate.Value.Date).TotalDays))
         .ForMember(dest => dest.ProductName, m => m.MapFrom(x => x.Product.ProdcutName))
         .ForMember(dest => dest.ProductDescription, m => m.MapFrom(x => x.Product.Description))
         .ForMember(dest => dest.ProductImages, m => m.MapFrom(x => x.Product.ProductImages.Select(x=>x.ProductImage)))
         .ForMember(dest => dest.NewPrice, m => m.MapFrom(x => (int)x.DiscountType == 1 ? x.Product.Price - x.DiscountValue : (x.Product.Price - (x.Product.Price * (x.DiscountValue / 100)))))
         .ForMember(dest => dest.DiscountRate, m => m.MapFrom(x => (int)x.DiscountType == 2 ? 
         x.DiscountRate : (100 * (x.Product.Price - x.DiscountValue) / x.Product.Price)
         ));
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<tblCities, CityDto>().ReverseMap();
            CreateMap<Rating, ShowRatingDto>().
                ForMember(x => x.ClientId, x => x.MapFrom(x => x.ClientId))
        .ForMember(x => x.ClientName, x => x.MapFrom(x => x.Client.User.FullName)).ReverseMap();
            CreateMap<Client, ClientDto>()
                .ForMember(x => x.FullName, x => x.MapFrom(x => x.User.FullName))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(x => x.User.PhoneNumber))
                .ForMember(x => x.Image, x => x.MapFrom(x => x.User.Image))
                .ReverseMap();
            CreateMap<Purchases, ShowPurchasesDto>()
              .ForMember(x => x.ApplicationUserId, x => x.MapFrom(x => x.Client.ApplicationUserId))
              .ForMember(x => x.CompanyName, x => x.MapFrom(x => x.Product.Company.User.FullName))
              .ForMember(x => x.DiscountId, x => x.MapFrom(x => x.DiscountId))
              .ForMember(x => x.RandomCode, x => x.MapFrom(x => x.Client.Code))
              .ForMember(x => x.NewPrice, x => x.MapFrom(x => x.NewPrice))
              .ForMember(x => x.PurchaseDate, x => x.MapFrom(x => x.PurchaseDate))
              .ForMember(x => x.SaleDate, x => x.MapFrom(x => x.SaleDate))
              .ForMember(x => x.quantity, x => x.MapFrom(x => x.quantity))
              .ForMember(x => x.ProductName, x => x.MapFrom(x => x.Product.ProdcutName))
              .ForMember(x => x.PhoneNumber, x => x.MapFrom(x => x.Client.User.PhoneNumber))
              .ReverseMap();
        }

        string getPath()
        {
            return "/wwwroot/UploadFiles/";
        }


    }
}