using AutoMapper;
using TooliRent.DTO_s.ReservationDTOs;
using TooliRent.Models;
using TooliRent.DTOs.AuthDTOs;

namespace TooliRent.Mapping
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            // CREATE: CreateReservationDTO -> Reservation
            CreateMap<CreateReservationDTO, Reservation>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active"))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // in service
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // in service
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // in service
                .ForMember(dest => dest.CanceledAt, opt => opt.Ignore())
                .ForMember(dest => dest.CanceledReason, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // UPDATE: UpdateReservationDTO -> Reservation  
            CreateMap<UpdateReservationDTO, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()) // in service
                .ForMember(dest => dest.CanceledAt, opt => opt.Ignore())
                .ForMember(dest => dest.CanceledReason, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // RESPONSE: Reservation -> ReservationSummaryDTO 
            CreateMap<Reservation, ReservationSummaryDTO>()
                .ForMember(dest => dest.TotalTools, opt => opt.MapFrom(src => src.ReservationTools.Count))
                .ForMember(dest => dest.EstimatedTotalPrice, opt => opt.MapFrom(src => src.ReservationTools.Sum(rt => rt.EstimatedPrice)))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));

            // RESPONSE: Reservation -> ReservationDetailDTO 
            CreateMap<Reservation, ReservationDetailDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Tools, opt => opt.MapFrom(src => src.ReservationTools.Select(rt => rt.Tool)))
                .ForMember(dest => dest.TotalTools, opt => opt.MapFrom(src => src.ReservationTools.Count))
                .ForMember(dest => dest.EstimatedTotalPrice, opt => opt.MapFrom(src => src.ReservationTools.Sum(rt => rt.EstimatedPrice)))
                .ForMember(dest => dest.DurationDays, opt => opt.MapFrom(src => (src.Date2Return - src.Date2Hire).Days + 1))
                .ForMember(dest => dest.CanBeCancelled, opt => opt.Ignore()) // in service
                .ForMember(dest => dest.CanBeModified, opt => opt.Ignore()) // in service
                .ForMember(dest => dest.Notes, opt => opt.Ignore()); // from ReservationTools

            // User mappning
            CreateMap<User, ReservationUserDTO>();
        }
    }
}
