using AutoMapper;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
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
            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Discount, ShowDiscountDto>().ReverseMap();
            CreateMap<Discount, ShowListProductDto>()
           .ForMember(dest=>dest.ProdcutDescription,m=>m.MapFrom(x=>x.Product.Description))
           .ForMember(dest=>dest.OldPrice,m=>m.MapFrom(x=>x.Product.Price))
           .ForMember(dest=>dest.ProductId,m=>m.MapFrom(x=>x.Product.Id))
           .ForMember(dest=>dest.ProdcutName,m=>m.MapFrom(x=>x.Product.ProdcutName))
           .ForMember(dest=>dest.ProductImage,m=>m.MapFrom(x=>x.Product.ProductImages.FirstOrDefault().ProductImage))
           .ForMember(dest=>dest.NewPrice,m=>m.MapFrom(x=> (int)x.DiscountType==1?x.Product.Price-x.DiscountValue:(x.Product.Price-(x.Product.Price * (x.DiscountValue / 100)))))
                ;

        }

        string getPath()
        {
            return "/wwwroot/UploadFiles/";
        }


    }
}