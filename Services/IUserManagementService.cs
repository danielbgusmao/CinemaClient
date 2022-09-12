using CinemaClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaClient.Services
{
    public interface IUserManagementService
    {
        Task<TokenResponse> LoginAsync(string email, string senha);        
    }
}
