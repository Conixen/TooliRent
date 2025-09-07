using AutoMapper;
using TooliRent.DTO_s.OrderDetailsDTOs;
using TooliRent.Models;
namespace TooliRent.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            // CREATE: CreateOrderDto -> OrderDetails
            CreateMap<CreateOrderDTO, OrderDeatils>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending")) 
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) 
                .ForMember(dest => dest.LateFee, opt => opt.MapFrom(src => 0)) 
                .ForMember(dest => dest.Date2Hire, opt => opt.Ignore()) 
                .ForMember(dest => dest.Date2Return, opt => opt.Ignore()); 

            // UPDATE: UpdateOrderDto -> OrderDetails
            CreateMap<UpdateOrderDTO, OrderDeatils>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) 
                .ForMember(dest => dest.ToolId, opt => opt.Ignore()) 
                .ForMember(dest => dest.ReservationId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Status, opt => opt.Ignore()) 
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) 
                .ForMember(dest => dest.LateFee, opt => opt.Ignore()) 
                .ForMember(dest => dest.Date2Hire, opt => opt.Ignore()) 
                .ForMember(dest => dest.Date2Return, opt => opt.Ignore()); 

            // RESPONSE: OrderDetails -> OrderSummaryDto 
            CreateMap<OrderDeatils, OrderSummaryDTO>()
                .ForMember(dest => dest.IsOverdue, opt => opt.Ignore()) 
                .ForMember(dest => dest.ToolName, opt => opt.Ignore()) 
                .ForMember(dest => dest.ToolBrand, opt => opt.Ignore()) 
                .ForMember(dest => dest.ToolModel, opt => opt.Ignore()) 
                .ForMember(dest => dest.UserName, opt => opt.Ignore()); 

            // RESPONSE: OrderDetails -> OrderDetailDto 
            CreateMap<OrderDeatils, OrderSummaryDTO>()
                           .ForMember(dest => dest.IsOverdue, opt => opt.Ignore()) 
                           .ForMember(dest => dest.ToolName, opt => opt.Ignore()) 
                           .ForMember(dest => dest.ToolBrand, opt => opt.Ignore()) 
                           .ForMember(dest => dest.ToolModel, opt => opt.Ignore()) 
                           .ForMember(dest => dest.UserName, opt => opt.Ignore()); 

            // SUPPORTING: User -> OrderUserDto
            CreateMap<User, OrderUserDTO>();

            // SUPPORTING: Reservation -> OrderReservationDto
            CreateMap<Reservation, OrderReservationDTO>()
                .ForMember(dest => dest.RentalDays, opt => opt.MapFrom(src => src.Date2Hire))
                .ForMember(dest => dest.IsOverdue, opt => opt.MapFrom(src => src.Date2Return));

        }
    }
}
