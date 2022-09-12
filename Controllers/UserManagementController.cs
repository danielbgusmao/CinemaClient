using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CinemaClient.Models;
using CinemaClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaClient.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("LoginPostAsync")]
        public async Task<IActionResult> LoginPostAsync(LoginViewModel viewModel)
        {
            var tokenResponse = await _userManagementService
                                .LoginAsync(viewModel.Email, viewModel.Senha);
            if (tokenResponse.Token == null)
            {
                TempData["MensagemErro"] = "Usuário ou senha inválido(s).";
                return Redirect("Index");
            }
            Response.Cookies.Append(
                Constants.XAccessToken,
                tokenResponse.Token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });
            return RedirectToAction("Index", "Home");
        }

    }
}
