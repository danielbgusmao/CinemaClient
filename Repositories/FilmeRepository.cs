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
    public class FilmeRepository : RepositoryBase, IFilmeRepository
    {
        public FilmeRepository(IFlurlClientFactory flurlClientFactory,
                                IHttpContextAccessor httpContextAccessor):
                                base(flurlClientFactory, httpContextAccessor)
        {
        }

        public async Task<List<Filme>> GetFilmes()
        {
            return await _flurlClient.Request("/Filme")
                            .GetJsonAsync<List<Filme>>();
        }

        public async Task<Filme> GetFilmeById(Guid id)
        {
            return await _flurlClient.Request($"/Filme/{id}")
                        .GetJsonAsync<Filme>();
        }

        public async Task<bool> Inserir(Filme filme)
        {
            var response = await _flurlClient.Request("/Filme")
            .PostJsonAsync(filme);

            var result = response.GetJsonAsync<bool>();

            return result.Result;
        }

        public async Task<bool> Atualizar(Filme filme)
        {
            var response = await _flurlClient.Request("/Filme")
            .PutJsonAsync(filme);

            var result = response.GetJsonAsync<bool>();

            return result.Result;
        }

        public async Task<bool> Remover(Guid id)
        {
            var response = await _flurlClient.Request($"/Filme/{id}").DeleteAsync();
            
            var result = response.GetJsonAsync<bool>();

            return result.Result;
        }


        public async Task<Filme> TituloEmUso(Filme filme)
        {
            var response = await _flurlClient.Request("/Filme/TituloEmUso")
            .PostJsonAsync(filme);

            var result = response.GetJsonAsync<Filme>();

            return (Filme)result.Result;

        }

    }
}
