using CsvHelper;
using DraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;

namespace DraftWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public bool login = false;
        public List<PokemonModel> TotalDraftDex;
        public List<PokemonModel> PlayerDraftDex;

        public HomeController(ILogger<HomeController> logger)
        {
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
            login = true;
            ViewBag.Login = login;
            ViewBag.Name = form["name"];
            ReadDraft();
            PlayerModel player = new PlayerModel(ViewBag.name, TotalDraftDex);
            PlayerDraftDex = player.Draft;
            ViewBag.Draft = PlayerDraftDex;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public List<PokemonModel> ReadDraft()
        {
            using (var reader = new StreamReader("Pokemon Dataset/Draft Pokemon Dataset.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var Pokemon = csv.GetRecords<PokemonModel>();
                Console.WriteLine(Pokemon.ToString());
                TotalDraftDex = Pokemon.ToList();
            }

            return TotalDraftDex;
        }
    }
}