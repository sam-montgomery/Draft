using CsvHelper;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;

namespace DraftWebApp.Models
{
    public static class PokeSession
    {
        public static string name { get; set; }
        public static bool login { get; set; }
        public static List<PokemonModel> playerDraft { get; set; }
        public static List<PokemonModel> team { get; set; }

        private static Dictionary<string, List<PokemonModel>> teampicks;
        private static Dictionary<string, List<PokemonModel>> playerDrafts;

        public static bool full = false;

        public static bool spick1 = false;
        public static PokemonModel smon1;
        public static bool spick2 = false;
        public static PokemonModel smon2;
        public static bool apick = false;
        public static PokemonModel amon;
        public static bool bpick = false;
        public static PokemonModel bmon;
        public static bool cpick = false;
        public static PokemonModel cmon;
        public static bool dpick = false;
        public static PokemonModel dmon;

        public static void InstatiateDraft(string username, string key) {
            name = username;
            login = true;
            full = false;

            List<PokemonModel> TotalDraftDex = new List<PokemonModel>();
            team = new List<PokemonModel>();
            int[] draftNumbers = new int[10];
            playerDraft = new List<PokemonModel>();

            using (var reader = new StreamReader("Pokemon Dataset/Draft Pokemon Dataset.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var Pokemon = csv.GetRecords<PokemonModel>();
                Console.WriteLine(Pokemon.ToString());
                TotalDraftDex = Pokemon.ToList();
            }

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

            foreach (PokemonModel pkmn in TotalDraftDex)
            {
                if (draftNumbers.Contains(pkmn.pokedex_number))
                {
                    pkmn.medialink = "https://www.smogon.com/dex/media/sprites/bw/" + pkmn.name.ToLower() + ".gif";
                    pkmn.sitelink = "https://www.smogon.com/dex/bw/pokemon/" + pkmn.name.ToLower();
                    char[] chartype = pkmn.type1.ToCharArray();
                    chartype[0] = char.ToUpper(chartype[0]);
                    pkmn.type1 = new string(chartype);
                    if (!string.IsNullOrEmpty(pkmn.type2))
                    {
                        chartype = pkmn.type2.ToCharArray();
                        chartype[0] = char.ToUpper(chartype[0]);
                        pkmn.type2 = new string(chartype);
                    }
                    playerDraft.Add(pkmn);
                }
            }

            teampicks[key] = new List<PokemonModel>();
            playerDrafts[key] = playerDraft;
        }

        public static List<PokemonModel> getPlayerDraft(string key)
        {
            return playerDrafts[key];
        }

        public static List<PokemonModel> getTeamPicks(string key)
        {
            return teampicks[key];
        }

        public static void addPokemon(int dexNo, string key)
        {
            if (full)
            {
                Console.WriteLine("Team full cannot add " + dexNo);
            }
            else
            {
                foreach(PokemonModel pkmn in playerDraft)
                {
                    if(pkmn.pokedex_number == dexNo)
                    {
                        switch(pkmn.tier) {
                            case "S":
                                if(spick1 && spick2)
                                {
                                    return;
                                }
                                if (spick1)
                                {
                                    smon2 = pkmn;
                                    spick2 = true;
                                }
                                else
                                {
                                    smon1 = pkmn;
                                    spick1 = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "A":
                                if (apick)
                                    return;
                                else
                                {
                                    amon = pkmn;
                                    apick = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "B":
                                if (bpick)
                                    return;
                                else
                                {
                                    bmon = pkmn;
                                    bpick = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "C":
                                if (cpick)
                                    return;
                                else
                                {
                                    cmon = pkmn;
                                    cpick = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "D":
                                if (dpick)
                                    return;
                                else
                                {
                                    dmon = pkmn;
                                    dpick = true;
                                }
                                team.Add(pkmn);
                                break;
                        }
                    }
                }
            }

            teampicks[key] = team;

            if (team.Count >= 6)
                full = true;
            else
                full = false;
        }

        public static void removePokemon(int dexNo)
        {
            if (team.Count == 0)
            {
                Console.WriteLine("Team empty cannot remove " + dexNo);
            }
            else
            {
                foreach (PokemonModel pkmn in team)
                {
                    if (pkmn.pokedex_number == dexNo)
                    {
                        switch (pkmn.tier)
                        {
                            case "S":
                                if (spick1 && spick2)
                                    spick2 = false;
                                else if (spick1)
                                    spick1 = false;
                                break;
                            case "A":
                                apick = false;
                                break;
                            case "B":
                                bpick = false;
                                break;
                            case "C":
                                cpick = false;
                                break;
                            case "D":
                                dpick = false;
                                break;
                        }
                        team.Remove(pkmn);
                        full = false;
                        return;
                    }
                }
            }
        }
    }
}
