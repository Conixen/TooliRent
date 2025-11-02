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

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(CancellationToken ct = default)
        {
            var users = await _userRepository.GetAllUserAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int userId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO?> UpdateUserAsync(int userId, UpdateUserDTO dto, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} not found.");

            await _userRepository.DeleteAsync(userId);
        }

        public async Task<AuthResponseDTO> RegisterAsync(CreateUserDTO dto, CancellationToken ct = default)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                return new AuthResponseDTO
                {
                    GreatSucessVeryNice = false,
                    Message = "Email already exists"
                };
            }

            var user = _mapper.Map<User>(dto);

            // TODO: Hash password properly (BCrypt)
            user.PasswordHash = dto.Password;

            var createdUser = await _userRepository.CreateAsync(user);

            return new AuthResponseDTO
            {
                GreatSucessVeryNice = true,
                Message = "User registered successfully",
                User = _mapper.Map<UserDTO>(createdUser)
            };
        }

        public async Task<AuthResponseDTO> LoginAsync(LogInDTO dto, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null || user.PasswordHash != dto.Password)
            {
                return new AuthResponseDTO
                {
                    GreatSucessVeryNice = false,
                    Message = "Invalid email or password"
                };
            }

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDTO
            {
                GreatSucessVeryNice = true,
                Message = "Login successful",
                Token = token,
                User = _mapper.Map<UserDTO>(user)
            };
        }
    }
}
