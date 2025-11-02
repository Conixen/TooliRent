using AutoMapper;
using TooliRent.DTOs.AuthDTOs;
using TooliRent.Models;
namespace TooliRent.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {

            CreateMap<User, UserDTO>();

            CreateMap<CreateUserDTO, User>();
 
            CreateMap<UpdateUserDTO, User>();

        }
    }
}
