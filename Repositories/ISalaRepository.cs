using CinemaClient.Entities;
using CinemaClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaClient.Repositories
{
    public interface ISalaRepository
    {
        Task<List<Sala>> GetSalas();

        Task<Sala> GetSalaById(Guid id);
    }
}
