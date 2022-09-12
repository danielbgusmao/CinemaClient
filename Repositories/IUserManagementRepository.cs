using CinemaClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaClient.Repositories
{
    public interface IUserManagementRepository
    {
        Task<TokenResponse> LoginAsync(string email, string senha);
    }
}
