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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending")) // Nya orders är pending
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.LateFee, opt => opt.MapFrom(src => 0)) // Börjar på 0
                .ForMember(dest => dest.Date2Hire, opt => opt.Ignore()) // Sätts vid checkout
                .ForMember(dest => dest.Date2Return, opt => opt.Ignore()); // Sätts vid return

            // UPDATE: UpdateOrderDto -> OrderDetails
            CreateMap<UpdateOrderDTO, OrderDeatils>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID ska inte ändras
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // UserId ska inte ändras
                .ForMember(dest => dest.ToolId, opt => opt.Ignore()) // ToolId ska inte ändras
                .ForMember(dest => dest.ReservationId, opt => opt.Ignore()) // ReservationId ska inte ändras
                .ForMember(dest => dest.Status, opt => opt.Ignore()) // Status ändras separat
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.LateFee, opt => opt.Ignore()) // Ändras separat
                .ForMember(dest => dest.Date2Hire, opt => opt.Ignore()) // Ändras separat
                .ForMember(dest => dest.Date2Return, opt => opt.Ignore()); // Ändras separat

            // RESPONSE: OrderDetails -> OrderSummaryDto (för listor)
            CreateMap<OrderDeatils, OrderSummaryDTO>()
                .ForMember(dest => dest.IsOverdue, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.ToolName, opt => opt.Ignore()) // Från Tool navigation
                .ForMember(dest => dest.ToolBrand, opt => opt.Ignore()) // Från Tool navigation
                .ForMember(dest => dest.ToolModel, opt => opt.Ignore()) // Från Tool navigation
                .ForMember(dest => dest.UserName, opt => opt.Ignore()); // Från User navigation

            // RESPONSE: OrderDetails -> OrderDetailDto (för enskild vy)
            CreateMap<OrderDeatils, OrderSummaryDTO>()
                           .ForMember(dest => dest.IsOverdue, opt => opt.Ignore()) // Beräknas i service
                           .ForMember(dest => dest.ToolName, opt => opt.Ignore()) // Från Tool navigation
                           .ForMember(dest => dest.ToolBrand, opt => opt.Ignore()) // Från Tool navigation
                           .ForMember(dest => dest.ToolModel, opt => opt.Ignore()) // Från Tool navigation
                           .ForMember(dest => dest.UserName, opt => opt.Ignore()); // Från User navigation

            // SUPPORTING: User -> OrderUserDto
            CreateMap<User, OrderUserDTO>();

            // SUPPORTING: Reservation -> OrderReservationDto
            CreateMap<Reservation, OrderReservationDTO>()
                .ForMember(dest => dest.RentalDays, opt => opt.MapFrom(src => src.Date2Hire))
                .ForMember(dest => dest.IsOverdue, opt => opt.MapFrom(src => src.Date2Return));

            // SUPPORTING: Tool -> återanvänd ToolSummaryDto (mappas i service)
            // Tool mappas via: mapper.Map<ToolSummaryDto>(order.Tool)

            // CANCEL: OrderCancelDto -> använd manuellt i service
            // CHECKOUT: CheckoutOrderDto -> använd manuellt i service
            // RETURN: ReturnOrderDto -> använd manuellt i service
        }
    }
}
