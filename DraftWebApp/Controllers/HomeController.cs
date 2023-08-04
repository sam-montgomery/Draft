using DraftWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DraftWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Default/{pokedex_number:int}")]
        public IActionResult Index(int pokedex_number)
        {
            bool removed = false;
            foreach (PokemonModel pkmn in PokeSession.getPlayerTeam(HttpContext.Session.GetString("__Key")))
            {
                if (pokedex_number == pkmn.pokedex_number)
                {
                    PokeSession.removePokemon(pokedex_number, HttpContext.Session.GetString("__Key"));
                    removed = true;
                    return View();
                }
            }

            if (!removed)
                PokeSession.addPokemon(pokedex_number, HttpContext.Session.GetString("__Key"));

            return View();
        }

        [HttpPost]
        public ActionResult Index(IFormCollection form)
        {
            GenerateUserKey(form["name"]);
            PokeSession.InstatiateDraft(form["name"], HttpContext.Session.GetString("__Key"));
            return View();
        }

        //Method for generating the users session key and storing it within the HTTPContext.
        public void GenerateUserKey(string username)
        {
            HttpContext.Session.SetString("__Name", username);
            HttpContext.Session.SetString("__Key", (username + "-" + DateTime.Now.TimeOfDay.ToString()));
        }
    }
}
