using AutoMapper;
using TooliRent.DTO_s.CategoryDTOs;
using TooliRent.Models;

namespace TooliRent.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDTO>();

            CreateMap<CreateCategoryDTO, Category>();
            //.ForMember(dest => dest.Id, opt => opt.Ignore())
            //.ForMember(dest => dest.Tools, opt => opt.Ignore());

            CreateMap<UpdateCategoryDTO, Category>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Tools, opt => opt.Ignore());
        }

    }
}
