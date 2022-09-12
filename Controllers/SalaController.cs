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
    public class SalaController : Controller
    {
        private readonly ILogger<SalaController> _logger;
        private readonly ISalaService _salaService;

        public SalaController(ILogger<SalaController> logger, ISalaService salaService)
        {
            _logger = logger;
            _salaService = salaService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new SalasViewModel
            {
                Salas = await _salaService.GetSalas()
            };

            return View(viewModel);
        }
    }
}
