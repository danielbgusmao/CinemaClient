using CinemaClient.Entities;
using CinemaClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaClient.Repositories
{
    public interface ISessaoRepository
    {
        Task<List<Sessao>> GetSessoes();

        Task<Sessao> GetSessaoById(Guid id);

        Task<Sessao> Inserir(Sessao sessao);

        Task<Sessao> Atualizar(Sessao sessao);

        Task Remover(Guid id);

        Task<Sessao> CalculaDataFim(Sessao sessao);

        Task<List<SessaoSugestaoDataInicioViewModel>> SugestaoDataInicio(Sessao sessao);

        Task<bool> VerificarSalaOcupada(Sessao sessao);

        Task<bool> ValidaDeleteSessao(Sessao sessao);

    }
}
