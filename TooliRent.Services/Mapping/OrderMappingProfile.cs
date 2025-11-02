using AutoMapper;
using TooliRent.DTO_s.OrderDetailsDTOs;
using TooliRent.Models;
namespace TooliRent.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderDeatils, OrderDetailsDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                    src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.Tool.Name))
                .ForMember(dest => dest.ToolBrand, opt => opt.MapFrom(src => src.Tool.Brand));

            CreateMap<CreateOrderDTO, OrderDeatils>();
            //.ForMember(dest => dest.Id, opt => opt.Ignore())
            //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Pending))
            //.ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            //.ForMember(dest => dest.LateFee, opt => opt.Ignore())
            //.ForMember(dest => dest.CheckedOutAt, opt => opt.Ignore())
            //.ForMember(dest => dest.ReturnedAt, opt => opt.Ignore())
            //.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            //.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            //.ForMember(dest => dest.UserId, opt => opt.Ignore())
            //.ForMember(dest => dest.User, opt => opt.Ignore())
            //.ForMember(dest => dest.Tool, opt => opt.Ignore());

            CreateMap<UpdateOrderDTO, OrderDeatils>();
                //.ForMember(dest => dest.Id, opt => opt.Ignore())
                //.ForMember(dest => dest.Status, opt => opt.Ignore())
                //.ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                //.ForMember(dest => dest.LateFee, opt => opt.Ignore())
                //.ForMember(dest => dest.CheckedOutAt, opt => opt.Ignore())
                //.ForMember(dest => dest.ReturnedAt, opt => opt.Ignore())
                //.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                //.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                //.ForMember(dest => dest.UserId, opt => opt.Ignore())
                //.ForMember(dest => dest.User, opt => opt.Ignore())
                //.ForMember(dest => dest.Tool, opt => opt.Ignore());
        }
    }
}
