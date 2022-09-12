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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Drawing;

namespace CinemaClient.Controllers
{
    public class SessaoController : Controller
    {
        private readonly ILogger<SessaoController> _logger;
        private readonly ISessaoService _sessaoService;
        private readonly IFilmeService _filmeService;
        private readonly ISalaService _salaService;

        public SessaoController(ILogger<SessaoController> logger, ISessaoService sessaoService, IFilmeService filmeService, ISalaService salaService)
        {
            _logger = logger;
            _sessaoService = sessaoService;
            _filmeService = filmeService;
            _salaService = salaService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new SessoesViewModel
            {
                Sessoes = await _sessaoService.GetSessoes()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            List<Sessao> sessaoList = await _sessaoService.GetSessoes();
            Sessao sessao = sessaoList.Where(x=>x.Id == id).FirstOrDefault();

            if (sessao == null)
            {
                return NotFound();
            }

            return View(sessao);
        }

        public IActionResult Create()
        {
            CarregaViewBagsCreate();

            return View();
        }

        private void CarregaViewBagsCreate()
        {
            ViewBag.Filmes = _filmeService.GetFilmes().Result.ToList();
            ViewBag.Salas = _salaService.GetSalas().Result.ToList();
        }


        private Sessao ConverteViewParaEntity(SessaoViewModel sessaoViewModel)
        {
            Sessao sessao = new()
            {
                TipoAnimacao = sessaoViewModel.TipoAnimacao,
                TipoAudio = sessaoViewModel.TipoAudio,
                ValorIngresso = float.Parse(sessaoViewModel.ValorIngresso),
                DataFim = sessaoViewModel.DataFim,
                DataInicio = sessaoViewModel.DataInicio,
                FilmeId = sessaoViewModel.FilmeId,
                SalaId = sessaoViewModel.SalaId,
                Sala = sessaoViewModel.Sala,
                Filme = sessaoViewModel.Filme,
                Id = sessaoViewModel.Id
            };

            return sessao;
        }

        private SessaoViewModel ConverteEntityParaView(Sessao sessao)
        {
            SessaoViewModel sessaoViewModel = new()
            {
                TipoAnimacao = sessao.TipoAnimacao,
                TipoAudio = sessao.TipoAudio,
                ValorIngresso = sessao.ValorIngresso.ToString(),
                DataFim = sessao.DataFim,
                DataInicio = sessao.DataInicio,
                FilmeId = sessao.FilmeId,
                SalaId = sessao.SalaId,
                Sala = sessao.Sala,
                Filme = sessao.Filme,
                Id = sessao.Id
            };

            return sessaoViewModel;
        }


        [HttpPost]
        public async Task<IActionResult> Create(SessaoViewModel sessaoViewModel)
        {
            Sessao sessao = ConverteViewParaEntity(sessaoViewModel);
            sessao.DataFim = CalculaDataFim(sessao).Result.DataFim;
            bool verificarSalaOcupada = _sessaoService.VerificarSalaOcupada(sessao).Result;

            if (sessao != null && !verificarSalaOcupada)
            {
                var result = await _sessaoService.Inserir(sessao);
                return View("SuccessCreate", result);
            }
            else if(verificarSalaOcupada)
            {
                CarregaViewBagsCreate();
                ViewData["msgDataInicio"] = "A sala está ocupada nessa data.";
                return View(sessaoViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            SessaoViewModel sessao = ConverteEntityParaView(await _sessaoService.GetSessaoById(id));
            
            if (sessao == null) return NotFound();
            
            CarregaViewBagsCreate();

            return View(sessao);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SessaoViewModel sessaoViewModel)
        {
            Sessao sessao = ConverteViewParaEntity(sessaoViewModel);
            sessao.DataFim = CalculaDataFim(sessao).Result.DataFim;
            bool verificarSalaOcupada = _sessaoService.VerificarSalaOcupada(sessao).Result;


            if (sessao == null) return NotFound();
            
            if (sessao != null && !verificarSalaOcupada)
            {
                try
                {
                    _sessaoService.Atualizar(sessao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_sessaoService.Existe(sessao.Id)) return NotFound();
                    else throw;
                }
                return View("SuccessUpdate", sessao);
            }
            return View(sessao);
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            if (_sessaoService == null) return NotFound();

            List<Sessao> sessaoList = await _sessaoService.GetSessoes();
            Sessao sessao = sessaoList.Where(x => x.Id == id).FirstOrDefault();

            if (sessao == null) return NotFound();

            ViewData["ValidaDeleteSessao"] = (_sessaoService.ValidaDeleteSessao(sessao).Result);

            return View(sessao);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_sessaoService == null) return Problem("Entity set 'sessaoservice' is null.");
            
            var sessao = await _sessaoService.GetSessaoById(id);

            var validaDeleteSessao = _sessaoService.ValidaDeleteSessao(sessao).Result;

            if (sessao != null && validaDeleteSessao) await _sessaoService.Remover(sessao.Id);
            
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public async Task<Sessao> CalculaDataFim(Sessao sessao)
        {
            return await _sessaoService.CalculaDataFim(sessao);
        }


        [HttpPost]
        public IActionResult SugestaoDataInicio(Sessao sessao)
        {
            List<SessaoSugestaoDataInicioViewModel> sugestao = _sessaoService.SugestaoDataInicio(sessao);

            return PartialView("SugestaoDataInicio", sugestao);
        }

    }
}
