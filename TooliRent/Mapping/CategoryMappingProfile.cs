using AutoMapper;
using TooliRent.DTO_s.CategoryDTOs;
using TooliRent.Models;

namespace TooliRent.Mapping
{
    public class CategoryMappingProfile : Profile
    {
            public CategoryMappingProfile()
            {
                // CREATE: CreateCategoryDto -> Category
                CreateMap<CreateCategoryDTO, Category>();

                // UPDATE: UpdateCategoryDto -> Category  
                CreateMap<UpdateCategoryDTO, Category>();

                // RESPONSE: Category -> CategoryDto
                CreateMap<Category, CategoryDto>();

                // SELECT: Category -> CategorySelectDto
                CreateMap<Category, CategorySelectDTO>()
                    .ForMember(dest => dest.ToolCount, opt => opt.Ignore()) // Sätts manuellt i service
                    .ForMember(dest => dest.HasAvailableTools, opt => opt.Ignore()); // Sätts manuellt i service
            }
        
    }
}
