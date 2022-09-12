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
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;

namespace CinemaClient.Repositories
{
    public class SessaoRepository : RepositoryBase, ISessaoRepository
    {
        public SessaoRepository(IFlurlClientFactory flurlClientFactory,
                                IHttpContextAccessor httpContextAccessor):
                                base(flurlClientFactory, httpContextAccessor)
        {
        }

        public async Task<List<Sessao>> GetSessoes()
        {
            return await _flurlClient.Request("/Sessao")
                            .GetJsonAsync<List<Sessao>>();
        }

        public async Task<Sessao> GetSessaoById(Guid id)
        {
            return await _flurlClient.Request($"/Sessao/{id}")
                        .GetJsonAsync<Sessao>();
        }

        public async Task<Sessao> Inserir(Sessao sessao)
        {
            var response = await _flurlClient.Request("/Sessao")
            .PostJsonAsync(sessao);

            var result = response.GetJsonAsync<Sessao>();

            return (Sessao)result.Result;
        }

        public async Task<Sessao> Atualizar(Sessao sessao)
        {
            return (Sessao)await _flurlClient.Request("/Sessao")
                        .PutJsonAsync(sessao);
        }

        public async Task Remover(Guid id)
        {
            await _flurlClient.Request($"/Sessao/{id}").DeleteAsync();
        }

        public async Task<Sessao> CalculaDataFim(Sessao sessao)
        {
            var response = await _flurlClient.Request("/Sessao/CalculaDataFim")
            .PostJsonAsync(sessao);

            var result = response.GetJsonAsync<Sessao>();

            return (Sessao)result.Result;
        }


        public async Task<List<SessaoSugestaoDataInicioViewModel>> SugestaoDataInicio(Sessao sessao)
        {
            var response = await _flurlClient.Request("/Sessao/SugestaoDataInicio")
            .PostJsonAsync(sessao);

            var result = response.GetJsonAsync<List<SessaoSugestaoDataInicioViewModel>>();

            return (List<SessaoSugestaoDataInicioViewModel>)result.Result;
        }


        public async Task<bool> VerificarSalaOcupada(Sessao sessao)
        {
            var response = await _flurlClient.Request("/Sessao/VerificarSalaOcupada")
            .PostJsonAsync(sessao);

            var result = response.GetJsonAsync<bool>();

            return (bool)result.Result;
        }

        public async Task<bool> ValidaDeleteSessao(Sessao sessao)
        {
            var response = await _flurlClient.Request("/Sessao/ValidaDeleteSessao")
            .PostJsonAsync(sessao);

            var result = response.GetJsonAsync<bool>();

            return (bool)result.Result;
        }

    }
}
