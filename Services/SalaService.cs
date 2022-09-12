using CinemaClient.Entities;
using CinemaClient.Models;
using CinemaClient.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaClient.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
        }


        public async Task<List<Sala>> GetSalas()
        {
            return await _salaRepository.GetSalas();
        }

        public async Task<Sala> GetSalaById(Guid id)
        {
            return await _salaRepository.GetSalaById(id);
        }
    }
}
