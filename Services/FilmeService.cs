using CinemaClient.Entities;
using CinemaClient.Models;
using CinemaClient.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaClient.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task<Filme> GetFilmeById(Guid id)
        {
            return await _filmeRepository.GetFilmeById(id);
        }

        public async Task<List<Filme>> GetFilmes()
        {
            return await _filmeRepository.GetFilmes();
        }

        public async Task<bool> Inserir(Filme filme)
        {
            await _filmeRepository.Inserir(filme);
            return true;
        }

        public async Task<bool> Atualizar(Filme filme)
        {
            await _filmeRepository.Atualizar(filme);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _filmeRepository.Remover(id);
            return true;
        }

        public bool Existe(Guid id)
        {
            return this.GetFilmeById(id) != null;
        }

        public async Task<Filme> TituloEmUso(Filme filme)
        {
            return await _filmeRepository.TituloEmUso(filme);
        }
    }
}
