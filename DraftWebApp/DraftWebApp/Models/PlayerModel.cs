namespace DraftWebApp.Models
{
    public class PlayerModel
    {
        public int[] DraftNumbers { get; set; }
        public int[] ExtendedDraftNumbers { get; set; }
        public List<PokemonModel> Draft { get; set; }

        public PlayerModel(string name, List<PokemonModel> DraftedPokemon) {
            Draft = new List<PokemonModel>();
            switch (name)
            {
                case "Scoliosis":
                    DraftNumbers = new int[] { 230, 59, 186, 20, 213, 474, 169, 232, 205, 119 };
                break;
                case "Sam":
                    DraftNumbers = new int[] { 468, 45, 73, 51, 162, 121, 473, 62, 18, 87 };
                break;
                case "Slim Jim":
                    DraftNumbers = new int[] { 462, 80, 38, 202, 132, 149, 214, 430, 463, 210 };
                break;
            }
            foreach(PokemonModel pkmn in DraftedPokemon)
            {
                if (DraftNumbers.Contains(pkmn.pokedex_number))
                {
                    pkmn.link = "https://www.smogon.com/dex/media/sprites/bw/" + pkmn.name.ToLower() + ".gif";
                    Draft.Add(pkmn);
                }
            }
        }
    }
}
