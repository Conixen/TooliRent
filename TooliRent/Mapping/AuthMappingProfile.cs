using AutoMapper;
using TooliRent.DTOs.AuthDTOs;
using TooliRent.Models;
namespace TooliRent.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            // REGISTER: RegisterUserDto -> User
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Member")) // Nya användare blir Members
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Hanteras separat i service (hashing)
                .ForMember(dest => dest.Reservations, opt => opt.Ignore()) // Navigation property
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); // Navigation property

            // UPDATE PROFILE: UpdateUserDto -> User
            CreateMap<UpdateUserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID ska inte ändras
                .ForMember(dest => dest.Email, opt => opt.Ignore()) // Email ändras separat
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Lösenord ändras separat
                .ForMember(dest => dest.Role, opt => opt.Ignore()) // Role ändras separat (admin only)
                .ForMember(dest => dest.Reservations, opt => opt.Ignore()) // Navigation property
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); // Navigation property

            // UPDATE ROLE: UpdateUserRoleDto -> User (admin only)
            CreateMap<UpdateUserStatusDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID ska inte ändras
                .ForMember(dest => dest.FirstName, opt => opt.Ignore()) // Namn ändras inte vid rolluppdatering
                .ForMember(dest => dest.LastName, opt => opt.Ignore()) // Namn ändras inte vid rolluppdatering
                .ForMember(dest => dest.Email, opt => opt.Ignore()) // Email ändras inte vid rolluppdatering
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Lösenord ändras inte vid rolluppdatering
                .ForMember(dest => dest.Reservations, opt => opt.Ignore()) // Navigation property
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); // Navigation property

            // UPDATE STATUS: UpdateUserStatusDto -> User (admin only)
            CreateMap<UpdateUserStatusDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID ska inte ändras
                .ForMember(dest => dest.FirstName, opt => opt.Ignore()) // Namn ändras inte vid statusuppdatering
                .ForMember(dest => dest.LastName, opt => opt.Ignore()) // Namn ändras inte vid statusuppdatering
                .ForMember(dest => dest.Email, opt => opt.Ignore()) // Email ändras inte vid statusuppdatering
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Lösenord ändras inte vid statusuppdatering
                .ForMember(dest => dest.Role, opt => opt.Ignore()) // Role ändras inte vid statusuppdatering
                .ForMember(dest => dest.Reservations, opt => opt.Ignore()) // Navigation property
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore()); // Navigation property

            // RESPONSE: User -> UserDto (för alla användarresponser)
            CreateMap<User, UserDTO>();

            // AUTH RESPONSES: Byggs manuellt i AuthService
            // AuthResponseDto byggs manuellt eftersom den innehåller tokens + UserDto
            // LoginDto används manuellt för verifiering
            // PasswordResetDto används manuellt för lösenordshantering
            // ChangePasswordDto används manuellt för lösenordsändring

            // SUPPORTING: Återanvänd från andra mappningar
            // User -> ReservationUserDto (mappas i ReservationMappingProfile)
            // User -> OrderUserDto (mappas i OrderMappingProfile)
        }
    }
}
