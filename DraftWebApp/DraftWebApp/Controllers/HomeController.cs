using CsvHelper;
using DraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;

namespace DraftWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public bool login = false;

        public HomeController(ILogger<HomeController> logger)
        {
            PlayerModel testplayer = new PlayerModel("Scoliosis");
            _logger = logger;   
        }

        public IActionResult Index()
        {
            ViewBag.Login = login;
            return View();
        }
        [HttpPost]
        public ActionResult Index(IFormCollection form)
        {
            GenerateUserKey(form["name"]);
            PokeSession.InstatiateDraft(form["name"], HttpContext.Session.GetString("__Key"));
            return View();
        }
        [HttpGet]
        [Route("Default/{pokedex_number:int}")]
        public IActionResult Index(int pokedex_number)
        {
            bool removed = false;
            foreach (PokemonModel pkmn in PokeSession.team)
            {
                if (pokedex_number == pkmn.pokedex_number)
                {
                    PokeSession.removePokemon(pokedex_number);
                    removed = true;
                    return View();
                }
            }
            
            if (!removed)
                PokeSession.addPokemon(pokedex_number);
            
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void GenerateUserKey(string username)
        {
            HttpContext.Session.SetString("__Name", username);
            HttpContext.Session.SetString("__Key", (username + "-" + DateTime.Now.TimeOfDay.ToString()));
        }
    }
}