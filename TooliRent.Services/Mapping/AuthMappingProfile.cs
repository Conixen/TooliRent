using AutoMapper;
using TooliRent.DTOs.AuthDTOs;
using TooliRent.Models;
namespace TooliRent.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            // REGISTER: RegisterUserDto = User
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Member"))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Reservations, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // UPDATE PROFILE: UpdateUserDto = User
            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Reservations, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // UPDATE ROLE: UpdateUserRoleDto = User (admin only)
            CreateMap<UpdateUserStatusDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.FirstName, opt => opt.Ignore()) 
                .ForMember(dest => dest.LastName, opt => opt.Ignore()) 
                .ForMember(dest => dest.Email, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
                .ForMember(dest => dest.Reservations, opt => opt.Ignore()) 
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); 

            // RESPONSE: User -> UserDto (for all users)
            CreateMap<User, UserDTO>();

        }
    }
}
