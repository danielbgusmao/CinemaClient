using CinemaClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CinemaClient.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        public async Task<TokenResponse> LoginAsync(string email, string senha)
        {
            var userLogin = new UserLogin
            {
                Email = email,
                Senha = senha
            };

            var retorno =  await "https://localhost:7103"
                     .AppendPathSegment("/Login")
                     .PostJsonAsync(userLogin).ReceiveJson<TokenResponse>();
            
            return retorno;
        }

    }
}
