using MatchArena.Domain.Entities;
using MatchArena.MVC.Models;
using MatchArena.MVC.Services.Interfaces;
using MatchArena.MVC.ViewModels;
using MatchArena.MVC.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;

namespace MatchArena.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlayerClientService _playerClient;
        private readonly ITeamClientService _teamClient;
        private readonly IFieldClientService _fieldClient;
        private readonly ITournamentClientService _tournamentClient;

        public HomeController(
            IPlayerClientService playerClient,
            ITeamClientService teamClient,
            IFieldClientService fieldClient,
            ITournamentClientService tournamentClient)
        {
            _playerClient = playerClient;
            _teamClient = teamClient;
            _fieldClient = fieldClient;
            _tournamentClient = tournamentClient;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeVM(
       await _playerClient.GetAllAsync(),
       await _teamClient.GetAllAsync(),
       await _fieldClient.GetAllAsync(),
       await _tournamentClient.GetAllAsync()
       );

            return View(vm);
        }
    }
}
