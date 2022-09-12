using CinemaClient.Entities;
using CinemaClient.Models;
using CinemaClient.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace CinemaClient.Services
{
    public class SessaoService : ISessaoService
    {
        private readonly ISessaoRepository _sessaoRepository;
        
        public SessaoService(ISessaoRepository sessaoRepository)
        {
            _sessaoRepository = sessaoRepository;
        }

        public async Task<Sessao> GetSessaoById(Guid id)
        {
            return await _sessaoRepository.GetSessaoById(id);
        }

        public async Task<List<Sessao>> GetSessoes()
        {
            return await _sessaoRepository.GetSessoes();
        }

        public async Task<Sessao> Inserir(Sessao sessao)
        {
            return await _sessaoRepository.Inserir(sessao);
        }

        public async Task<Sessao> Atualizar(Sessao sessao)
        {
            return await _sessaoRepository.Atualizar(sessao);
        }

        public async Task Remover(Guid id)
        {
            await _sessaoRepository.Remover(id);
        }

        public bool Existe(Guid id)
        {
            return this.GetSessaoById(id) != null;
        }

        public async Task<Sessao> CalculaDataFim(Sessao sessao)
        {
            return await _sessaoRepository.CalculaDataFim(sessao);
        }

        public List<SessaoSugestaoDataInicioViewModel> SugestaoDataInicio(Sessao sessao)
        {
            sessao = CalculaDataFim(sessao).Result;
            return _sessaoRepository.SugestaoDataInicio(sessao).Result;
        }

        public async Task<bool> VerificarSalaOcupada(Sessao sessao)
        {
            return await _sessaoRepository.VerificarSalaOcupada(sessao);
        }

        public async Task<bool> ValidaDeleteSessao(Sessao sessao)
        {
            return await _sessaoRepository.ValidaDeleteSessao(sessao);
        }


    }
}
