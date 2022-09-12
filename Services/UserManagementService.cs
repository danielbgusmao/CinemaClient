using CinemaClient.Models;
using CinemaClient.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaClient.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public async Task<TokenResponse> LoginAsync(string email, string senha)
        {
            return await _userManagementRepository.LoginAsync(email, senha);
        }

    }
}
