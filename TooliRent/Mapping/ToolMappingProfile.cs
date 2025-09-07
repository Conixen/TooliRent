using AutoMapper;
using TooliRent.DTO_s.CategoryDTOs;
using TooliRent.DTO_s.ToolsDTOs;
using TooliRent.Models;
namespace TooliRent.Mapping
{
    public class ToolMappingProfile : Profile
    {
        public ToolMappingProfile()
        {
            // CREATE: CreateToolDto -> Tool
            CreateMap<CreateToolDto, Tool>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Available")) // Nya verktyg är tillgängliga
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true)); // Nya verktyg är tillgängliga

            // UPDATE: UpdateToolDto -> Tool
            CreateMap<UpdateToolDTO, Tool>();

            // UPDATE STATUS: UpdateToolStatusDto -> Tool
            CreateMap<UpdateToolStatusDTO, Tool>()
                .ForMember(dest => dest.Name, opt => opt.Ignore()) // Ändra inte namn vid statusuppdatering
                .ForMember(dest => dest.Brand, opt => opt.Ignore()) // Ändra inte brand
                .ForMember(dest => dest.Model, opt => opt.Ignore()) // Ändra inte model
                .ForMember(dest => dest.PricePerDay, opt => opt.Ignore()) // Ändra inte pris
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()); // Ändra inte kategori

            // RESPONSE: Tool -> ToolSummaryDto (för listor)
            CreateMap<Tool, ToolSummaryDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore()); // Sätts manuellt i service

            // RESPONSE: Tool -> ToolDetailDto (för enskild vy)
            CreateMap<Tool, ToolDetailDTO>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()) // Sätts manuellt i service
                .ForMember(dest => dest.IsAvailable, opt => opt.Ignore()); // Sätts manuellt i service

            // RESPONSE: Tool -> ToolSelectDto (för dropdown)
            CreateMap<Tool, ToolSelectDTO>();

            // SUPPORTING: Category -> ToolCategoryDto
            CreateMap<Category, ToolCategoryDTO>();

        }
    }
}
