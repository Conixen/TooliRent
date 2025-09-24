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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Available"))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // UPDATE: UpdateToolDto -> Tool
            CreateMap<UpdateToolDTO, Tool>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // UPDATE STATUS: UpdateToolStatusDto -> Tool
            CreateMap<UpdateToolStatusDTO, Tool>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Brand, opt => opt.Ignore())
                .ForMember(dest => dest.Model, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.PricePerDay, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
                .ForMember(dest => dest.SerialNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // RESPONSE: Tool -> ToolSummaryDto
            CreateMap<Tool, ToolSummaryDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // RESPONSE: Tool -> ToolDetailDto
            CreateMap<Tool, ToolDetailDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            // RESPONSE: Tool -> ToolSelectDto (dropdown)
            CreateMap<Tool, ToolSelectDTO>();

            // SUPPORTING: Category -> ToolCategoryDto
            CreateMap<Category, ToolCategoryDTO>();
        }
    }
}
