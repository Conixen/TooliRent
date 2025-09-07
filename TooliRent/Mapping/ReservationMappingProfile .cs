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
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Active")) 
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore()) 
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); 

            // UPDATE: UpdateReservationDto -> Reservation  
            CreateMap<UpdateReservationDTO, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Status, opt => opt.Ignore()) 
                .ForMember(dest => dest.ReservationTools, opt => opt.Ignore()) 
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); 

            // RESPONSE: Reservation -> ReservationSummaryDto 
            CreateMap<Reservation, ReservationSummaryDTO>()
                .ForMember(dest => dest.TotalTools, opt => opt.Ignore()) 
                .ForMember(dest => dest.EstimatedTotalPrice, opt => opt.Ignore()) 
                .ForMember(dest => dest.UserName, opt => opt.Ignore());

            // RESPONSE: Reservation -> ReservationDetailDto 
            CreateMap<Reservation, ReservationDetailDTO>()
                .ForMember(dest => dest.User, opt => opt.Ignore()) 
                .ForMember(dest => dest.Tools, opt => opt.Ignore()) 
                .ForMember(dest => dest.TotalTools, opt => opt.Ignore()) 
                .ForMember(dest => dest.EstimatedTotalPrice, opt => opt.Ignore()) 
                .ForMember(dest => dest.DurationDays, opt => opt.Ignore()) 
                .ForMember(dest => dest.CanBeCancelled, opt => opt.Ignore()) 
                .ForMember(dest => dest.CanBeModified, opt => opt.Ignore()); 

            CreateMap<User, ReservationUserDTO>();
            
        }
    }
}
