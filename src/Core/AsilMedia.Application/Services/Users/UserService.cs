﻿using AsilMedia.Application.Abstractions.Repositories;
using AsilMedia.Application.DataTransferObjects;
using AsilMedia.Application.Halpers.JWTServices;
using AsilMedia.Domain.Entities;

namespace AsilMedia.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IJWTService _jWTService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IJWTService jWTService, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _jWTService = jWTService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public Task<User> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> InsertAsync(UserDTO userDTO)
        {
            var user = new User()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                RoleId = userDTO.RoleId,
                RefreshToken = userDTO.RefreshToken,
            };

            user = await _userRepository.InsertAsync(user);
            var role = await _roleRepository.SelectByIdAsync(user.RoleId);

            user.Role = role;

            var token = _jWTService.GenerateAccessToken(user);

            return token;
        }

        public async Task<List<User>> SelectAllAsync()
            => await _userRepository.SelectAllAsync();

        public async Task<User> SelectByIdAsync(long id)
            => await _userRepository.SelectByIdAsync(id);

        public Task<User> UpdateAsync(UserDTO userDTO, long id)
        {
            throw new NotImplementedException();
        }
    }
}
