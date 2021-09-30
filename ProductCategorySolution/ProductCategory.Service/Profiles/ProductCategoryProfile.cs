using AutoMapper;
using MongoDB.Bson;
using ProductCategory.Infrastructure.Models;
using ProductCategory.Service.Models;

namespace ProductCategory.Service.Profiles
{
    /// <summary>
    /// Profile class for casting between product models.
    /// </summary>
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(p_dto => p_dto.Id, y => y.MapFrom(dto => dto.Id.ToString()))
                .ForMember(p_dto => p_dto.CategoryId, y => y.Ignore());

            CreateMap<ProductRequestDto, Product>()
                .ForMember(p => p.Id, y => y.MapFrom(p_dto => ObjectId.GenerateNewId()));

            CreateMap<Category, CategoryResponseDto>()
                .ForMember(c_dto => c_dto.Id, y => y.MapFrom(c => c.Id.ToString()));

            CreateMap<CategoryRequestDto, Category>()
                .ForMember(c => c.Id, y => y.MapFrom(c_dto => ObjectId.GenerateNewId()));
        }
    }
}
