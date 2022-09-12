using CinemaClient.Entities;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Http;
using CinemaClient.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CinemaClient.Repositories
{
    public class SalaRepository : RepositoryBase, ISalaRepository
    {
        public SalaRepository(IFlurlClientFactory flurlClientFactory,
                                IHttpContextAccessor httpContextAccessor):
                                base(flurlClientFactory, httpContextAccessor)
        {
        }

        public async Task<List<Sala>> GetSalas()
        {
            return await _flurlClient.Request("/Sala")
                            .GetJsonAsync<List<Sala>>();
        }

        public async Task<Sala> GetSalaById(Guid id)
        {
            return await _flurlClient.Request($"/Sala/{id}")
                        .GetJsonAsync<Sala>();
        }

    }
}
