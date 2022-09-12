using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CinemaClient.Models;
using CinemaClient.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CinemaClient.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CinemaClient.Controllers
{
    public class FilmeController : Controller
    {
        private readonly ILogger<FilmeController> _logger;
        private readonly IFilmeService _filmeService;
        private readonly ISessaoService _sessaoService;

        public FilmeController(ILogger<FilmeController> logger, IFilmeService filmeService, ISessaoService sessaoService)
        {
            _logger = logger;
            _filmeService = filmeService;
            _sessaoService = sessaoService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new FilmesViewModel { Filmes = await _filmeService.GetFilmes() };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            Filme filme = await _filmeService.GetFilmeById(id);

            if (filme == null) return NotFound();

            return View(filme);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        public class FileUploadAPI
        {
            public IFormFile Imagem { get; set; }
        }

        private static byte[] ImagemToBytes(FileUploadAPI imagem)
        {
            using var fileStream = imagem.Imagem.OpenReadStream();
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)imagem.Imagem.Length);
            return bytes;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Titulo, Descricao, Duracao, Imagem")] Filme filme, [FromForm] FileUploadAPI imagem)
        {
            filme.Imagem = ImagemToBytes(imagem);

            if(imagem.Imagem.Length > 0)
            {
                var verifica = TituloEmUso(filme).Result;

                if (verifica == null)
                    await _filmeService.Inserir(filme);
                else
                {
                    ViewData["msgTitulo"] = verifica.Id;
                    return View(filme);
                }
            }
            else return NotFound();

            return View("SuccessCreate", filme);
        }

        
        public async Task<IActionResult> Edit(Guid id)
        {
            Filme filme = await _filmeService.GetFilmeById(id);
            
            if (filme == null) return NotFound(); 

            return View(filme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, Titulo, Descricao, Duracao, Imagem")] Filme filme, [FromForm] FileUploadAPI imagem)
        {
            if (filme == null) return NotFound(); 

            filme.Imagem = ImagemToBytes(imagem);

            if (imagem.Imagem.Length > 0)
            {
                try
                {
                    var verificaTitulo = TituloEmUso(filme).Result;

                    if (verificaTitulo == null || verificaTitulo.Id == filme.Id)
                        await _filmeService.Atualizar(filme);
                    else
                    {
                        ViewData["msgTitulo"] = verificaTitulo.Id;
                        return View(filme);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_filmeService.Existe(filme.Id))  return NotFound();
                    else throw;
                }
                return View("SuccessUpdate", filme);
            }
            return View(filme);
        }

        
        public async Task<IActionResult> Delete(Guid id)
        {
            if (_filmeService == null) return NotFound();

            Filme filme = await _filmeService.GetFilmeById(id);

            if (filme == null) return NotFound();
            else { filme.Sessoes = _sessaoService.GetSessoes().Result.Where(x => x.FilmeId == filme.Id).ToList(); }

            return View(filme);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_filmeService == null) return Problem("Entity set 'filmeService' is null.");
            
            var filme = await _filmeService.GetFilmeById(id);
            var qtdSessoes = (_sessaoService.GetSessoes().Result).Where(x => x.FilmeId == filme.Id).Count();

            if (filme != null && qtdSessoes == 0)
                await _filmeService.Remover(filme.Id);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<Filme> TituloEmUso(Filme filme)
        {
            return await _filmeService.TituloEmUso(filme);

        }

    }
}
