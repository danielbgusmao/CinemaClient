using CinemaClient.Entities;
using CinemaClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaClient.Services
{
    public interface IFilmeService
    {
        Task<List<Filme>> GetFilmes();
        
        Task<Filme> GetFilmeById(Guid id);

        Task<bool> Inserir(Filme filme);

        Task<bool> Atualizar(Filme filme);

        Task<bool> Remover(Guid id);

        bool Existe(Guid id);

        Task<Filme> TituloEmUso(Filme filme);
    }
}
