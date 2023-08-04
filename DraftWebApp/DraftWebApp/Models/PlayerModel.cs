using CsvHelper;
using System.Globalization;

namespace DraftWebApp.Models
{
    public class PlayerModel
    {
        public int[] draftNumbers { get; set; }
        public int[] extendedDraftNumbers { get; set; }
        public List<PokemonModel> draft { get; set; }
        public TeamBuilderModel team { get; set; }

        public PlayerModel(string name) {

            team = new TeamBuilderModel();

            List<PokemonModel> TotalDraftDex = new List<PokemonModel>();
            using (var reader = new StreamReader("Pokemon Dataset/Draft Pokemon Dataset.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var Pokemon = csv.GetRecords<PokemonModel>();
                Console.WriteLine(Pokemon.ToString());
                TotalDraftDex = Pokemon.ToList();
            }

            draft = new List<PokemonModel>();
            switch (name)
            {
                case "Scoliosis":
                    draftNumbers = new int[] { 230, 59, 186, 20, 213, 474, 169, 232, 205, 119 };
                break;
                case "Sam":
                    draftNumbers = new int[] { 468, 45, 73, 51, 162, 121, 473, 62, 18, 87 };
                break;
                case "Slim Jim":
                    draftNumbers = new int[] { 462, 80, 38, 202, 132, 149, 214, 430, 463, 210 };
                break;
            }
            foreach(PokemonModel pkmn in TotalDraftDex)
            {
                if (draftNumbers.Contains(pkmn.pokedex_number))
                {
                    pkmn.medialink = "https://www.smogon.com/dex/media/sprites/bw/" + pkmn.name.ToLower() + ".gif";
                    pkmn.sitelink = "https://www.smogon.com/dex/bw/pokemon/" + pkmn.name.ToLower();
                    draft.Add(pkmn);
                }
            }
        }
    }
}
