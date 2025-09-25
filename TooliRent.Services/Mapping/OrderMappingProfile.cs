using AutoMapper;
using TooliRent.DTO_s.OrderDetailsDTOs;
using TooliRent.Models;
namespace TooliRent.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            // CREATE: CreateOrderDTO -> OrderDeatils
            CreateMap<CreateOrderDTO, OrderDeatils>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"))
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // Calculated in service
                .ForMember(dest => dest.LateFee, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.ReservationId, opt => opt.Ignore())
                .ForMember(dest => dest.CheckedOutAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReturnedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Tool, opt => opt.Ignore())
                .ForMember(dest => dest.Reservation, opt => opt.Ignore());

            // UPDATE: UpdateOrderDTO -> OrderDeatils
            CreateMap<UpdateOrderDTO, OrderDeatils>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.LateFee, opt => opt.Ignore())
                .ForMember(dest => dest.CheckedOutAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReturnedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Tool, opt => opt.Ignore())
                .ForMember(dest => dest.Reservation, opt => opt.Ignore());

            // RESPONSE: OrderDeatils -> OrderSummaryDTO 
            CreateMap<OrderDeatils, OrderSummaryDTO>()
                .ForMember(dest => dest.IsOverdue, opt => opt.MapFrom(src =>
                    src.Date2Return < DateTime.UtcNow && src.Status != "Returned"))
                .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.Tool.Name))
                .ForMember(dest => dest.ToolBrand, opt => opt.MapFrom(src => src.Tool.Brand))
                .ForMember(dest => dest.ToolModel, opt => opt.MapFrom(src => src.Tool.Model))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                    $"{src.User.FirstName} {src.User.LastName}"));

            // SUPPORTING: User -> OrderUserDTO
            CreateMap<User, OrderUserDTO>();

            // SUPPORTING: Tool -> OrderToolDTO
            CreateMap<Tool, OrderToolDTO>();

            // SUPPORTING: Reservation -> OrderReservationDTO
            CreateMap<Reservation, OrderReservationDTO>()
                .ForMember(dest => dest.RentalDays, opt => opt.MapFrom(src =>
                    (src.Date2Return - src.Date2Hire).Days + 1))
                .ForMember(dest => dest.IsOverdue, opt => opt.MapFrom(src =>
                    src.Date2Return < DateTime.UtcNow && src.Status == "Active"))
                .ForMember(dest => dest.DaysOverdue, opt => opt.MapFrom(src =>
                    src.Date2Return < DateTime.UtcNow && src.Status == "Active"
                        ? (DateTime.UtcNow - src.Date2Return).Days
                        : 0))
                .ForMember(dest => dest.CanBeCancelled, opt => opt.Ignore())
                .ForMember(dest => dest.CanBeCheckedOut, opt => opt.Ignore())
                .ForMember(dest => dest.CanBeReturned, opt => opt.Ignore());
        }
    }
}
