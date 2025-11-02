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
            // Tool → ToolDTO (minimalt - ingen serialNumber, categoryId)
            CreateMap<Tool, ToolDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateToolDTO, Tool>();
            CreateMap<UpdateToolDTO, Tool>();

        }
    }
}
