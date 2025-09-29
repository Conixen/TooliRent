using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TooliRent.Core.Interfaces.IRepository;
using TooliRent.Core.Interfaces.IService;
using TooliRent.DTOs.AuthDTOs;
using TooliRent.Models;
using TooliRent.Services.Auth;

namespace TooliRent.Services.Service
{
    public class UserService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJWTTokenService _jwtTokenService;
        public UserService(IUserRepository userRepository, IMapper mapper, IJWTTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<AuthResponseDTO> RegisterAsync(CreateUserDTO dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = dto.Password; 

            var createdUser = await _userRepository.AddAsync(user);

            return new AuthResponseDTO
            {
                Success = true,
                Message = "User registered successfully",
                User = _mapper.Map<UserDTO>(createdUser)
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(LogInDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            // Enkel lösenordskontroll (senare BCrypt.Verify)
            if (user == null || user.PasswordHash != dto.Password)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            // Generera JWT token
            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDTO
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Expires = DateTime.UtcNow.AddHours(2),
                User = _mapper.Map<UserDTO>(user)
            };
        }

        public async Task<UserDTO> GetProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task UpdateProfileAsync(int userId, UpdateUserDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new ArgumentException("User not found");

            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(int userId, ForgotPasswordDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new ArgumentException("User not found");

            user.PasswordHash = dto.NewPassword;
            await _userRepository.UpdateAsync(user);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDTO dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
                return;

            
            user.PasswordHash = dto.NewPassword;
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateUserStatusAsync(int userId, UpdateUserStatusDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new ArgumentException("User not found");

            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);
        }

    }
}
