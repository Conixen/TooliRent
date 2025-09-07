using AutoMapper;
using TooliRent.DTO_s.ReservationDTOs;
using TooliRent.Models;

namespace TooliRent.Mapping
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            // CREATE: CreateReservationDto -> Reservation
            CreateMap<CreateReservationDTO, Reservation>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active")) // Nya reservationer är aktiva
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore()) // Hanteras separat i service
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); // Navigation property

            // UPDATE: UpdateReservationDto -> Reservation  
            CreateMap<UpdateReservationDTO, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID ska inte ändras
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // UserId ska inte ändras
                .ForMember(dest => dest.Status, opt => opt.Ignore()) // Status ändras separat
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore()) // Hanteras separat
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); // Navigation property

            // RESPONSE: Reservation -> ReservationSummaryDto (för listor)
            CreateMap<Reservation, ReservationSummaryDTO>()
                .ForMember(dest => dest.TotalTools, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.EstimatedTotalPrice, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.UserName, opt => opt.Ignore()); // Sätts från User navigation

            // RESPONSE: Reservation -> ReservationDetailDto (för enskild vy)
            CreateMap<Reservation, ReservationDetailDTO>()
                .ForMember(dest => dest.User, opt => opt.Ignore()) // Mappas separat
                .ForMember(dest => dest.Tools, opt => opt.Ignore()) // Mappas från ReservationTools
                .ForMember(dest => dest.TotalTools, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.EstimatedTotalPrice, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.DurationDays, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.CanBeCancelled, opt => opt.Ignore()) // Beräknas i service
                .ForMember(dest => dest.CanBeModified, opt => opt.Ignore()); // Beräknas i service

            CreateMap<User, ReservationUserDTO>();
            // CANCEL: ReservationCancelDto -> använd manuellt i service
            // (Ingen direkt mappning - hanteras genom att sätta status och anledning)

            // SUPPORTING: User -> ReservationUserDto

            // SUPPORTING: ReservationTool + Tool -> ToolSummaryDto (hanteras i service)
            // Tools mappas via: reservation.ReservationTools.Select(rt => mapper.Map<ToolSummaryDto>(rt.Tool))
        }
    }
}
