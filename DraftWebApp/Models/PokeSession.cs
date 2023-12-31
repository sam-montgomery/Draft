﻿using CsvHelper;
using System.Globalization;

namespace DraftWebApp.Models
{
    public class PokeSession
    {
        //Contains the teams and tracks the drafts of the 3 users. Each static member tracks the users information through their user session key.
        public static Dictionary<string, string> name = new Dictionary<string,string>();
        public static Dictionary<string, bool> login = new Dictionary<string,bool>();

        public static bool totalDraftLoaded = false;
        public static List<PokemonModel> totalDraftDex;

        private static Dictionary<string, List<PokemonModel>> playerTeams = new Dictionary<string, List<PokemonModel>>(); //Team created from the draft by the user.

        private static Dictionary<string, List<PokemonModel>> playerDrafts = new Dictionary<string, List<PokemonModel>>(); //Lists containing the drafts of each user.

        //Members for team creating logic:
        public static Dictionary<string, bool> full = new Dictionary<string, bool>();

        public static Dictionary<string, bool> spick1s = new Dictionary<string, bool>();
        public static Dictionary<string, PokemonModel> smon1s = new Dictionary<string, PokemonModel>();
        public static Dictionary<string, bool> spick2s = new Dictionary<string, bool>();
        public static Dictionary<string, PokemonModel> smon2s = new Dictionary<string, PokemonModel>();
        public static Dictionary<string, bool> apicks = new Dictionary<string, bool>();
        public static Dictionary<string, PokemonModel> amons = new Dictionary<string, PokemonModel>();
        public static Dictionary<string, bool> bpicks = new Dictionary<string, bool>();
        public static Dictionary<string, PokemonModel> bmons = new Dictionary<string, PokemonModel>();
        public static Dictionary<string, bool> cpicks = new Dictionary<string, bool>();
        public static Dictionary<string, PokemonModel> cmons = new Dictionary<string, PokemonModel>();
        public static Dictionary<string, bool> dpicks = new Dictionary<string, bool>();
        public static Dictionary<string, PokemonModel> dmons = new Dictionary<string, PokemonModel>();

        //Members for team match-up logic.
        public static Dictionary<string, Dictionary<string, float>> teamMatchupUps = new Dictionary<string, Dictionary<string, float>>();

        //Bool to tell if theres any pokemon in the team. 
        public static Dictionary<string, bool> empty = new Dictionary<string, bool>();


        //Method for populating the users team for the first time. 
        public static void InstatiateDraft(string username, string key)
        {
            if (!totalDraftLoaded)
                LoadPokemon();

            //First time set up for the user.
            login[key] = true;
            name[key] = username;
            full[key] = false;
            empty[key] = true;


            //Setting the team logic variables to allow the user to pick their team.
            spick1s[key] = false;
            spick2s[key] = false;
            apicks[key] = false;
            bpicks[key] = false;
            cpicks[key] = false;
            dpicks[key] = false;


            //Int array to select the correct pokemon for each user. 
            int[] draftNumbers = new int[10];

                //Assign the draft numbers.
                switch (username) {
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

            //Temporary draft list to send to dictionary. 
            List<PokemonModel> draft = new List<PokemonModel>();

            //Generating the PokemonModels of each of the selected Pokemon. 
            foreach (PokemonModel pkmn in totalDraftDex)
            {
                if (draftNumbers.Contains(pkmn.pokedex_number))
                {
                    //Adding the media and the smogon link to each PokemonModel.
                    pkmn.medialink = "https://www.smogon.com/dex/media/sprites/bw/" + pkmn.name.ToLower() + ".gif";
                    pkmn.sitelink = "https://www.smogon.com/dex/bw/pokemon/" + pkmn.name.ToLower();

                    //Correcting the capitalisation of the type in the PokemonModel.
                    char[] chartype = pkmn.type1.ToCharArray();
                    chartype[0] = char.ToUpper(chartype[0]);
                    pkmn.type1 = new string(chartype);

                    //Null check for the second type.
                    if (!string.IsNullOrEmpty(pkmn.type2))
                    {
                        chartype = pkmn.type2.ToCharArray();
                        chartype[0] = char.ToUpper(chartype[0]);
                        pkmn.type2 = new string(chartype);
                    }

                    //Add the PokemonModel to the drafts dictionary with the users session key. 
                    draft.Add(pkmn);
                }
            }
            //Setting the users draft to the temporary draft variable.
            playerDrafts[key] = draft;
            //Setting the users team to an empty list. 
            playerTeams[key] = new List<PokemonModel>();

            teamMatchupUps[key] = new Dictionary<string, float>();
        }

        //Method for adding Pokemon to players team, contains tiering logic.
        public static void addPokemon(int dexNo, string key)
        {
            //If the players team is not full.
            if (!full[key])
            {
                List<PokemonModel> team = playerTeams[key];
                foreach(PokemonModel pkmn in playerDrafts[key])
                {
                    //If the desired Pokemon is contained in the players draft
                    if(pkmn.pokedex_number == dexNo)
                    {
                        //Determine the Pokemons tier.
                        switch (pkmn.tier)
                        {
                            case "S":
                                if (spick1s[key] && spick2s[key])
                                {
                                    return;
                                }
                                if (spick1s[key])
                                {
                                    smon2s[key] = pkmn;
                                    spick2s[key] = true;
                                }
                                else
                                {
                                    smon1s[key] = pkmn;
                                    spick1s[key] = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "A":
                                if (apicks[key])
                                    return;
                                else
                                {
                                    amons[key] = pkmn;
                                    apicks[key] = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "B":
                                if (bpicks[key])
                                    return;
                                else
                                {
                                    bmons[key] = pkmn;
                                    bpicks[key] = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "C":
                                if (cpicks[key])
                                    return;
                                else
                                {
                                    cmons[key] = pkmn;
                                    cpicks[key] = true;
                                }
                                team.Add(pkmn);
                                break;
                            case "D":
                                if (dpicks[key])
                                    return;
                                else
                                {
                                    dmons[key] = pkmn;
                                    dpicks[key] = true;
                                }
                                team.Add(pkmn);
                                break;
                        }
                    }
                }
                //Resetting the players team. 
                empty[key] = false;
                playerTeams[key] = team;
                if (team.Count >= 6)
                    full[key] = true;
                else
                    full[key] = false;

                calculateTypeMatchup(key);
            }
        }

        //Method for removing Pokemon from the players team based on their user session key. 
        public static void removePokemon(int dexNo, string key)
        {
            if (playerTeams[key].Count != 0)
            {
                List<PokemonModel> team = playerTeams[key];
                foreach(PokemonModel pkmn in team)
                {
                    if (pkmn.pokedex_number == dexNo)
                    {
                        switch (pkmn.tier)
                        {
                            case "S":
                                if (spick1s[key] && spick2s[key])
                                    spick2s[key] = false;
                                else if (spick1s[key])
                                    spick1s[key] = false;
                                break;
                            case "A":
                                apicks[key] = false;
                                break;
                            case "B":
                                bpicks[key] = false;
                                break;
                            case "C":
                                cpicks[key] = false;
                                break;
                            case "D":
                                dpicks[key] = false;
                                break;
                        }
                        team.Remove(pkmn);
                        playerTeams[key] = team;
                        full[key] = false;
                        if (team.Count == 0)
                            empty[key] = true;
                        calculateTypeMatchup(key);
                        return;
                    }
                }
            }
        }

        //Method for calculating type match-ups for current team members.
        public static void calculateTypeMatchup(string key)
        {
            Dictionary<string, float> matchup = new Dictionary<string, float>();
            bool first = true;
            foreach(PokemonModel pkmn in playerTeams[key])
            {
                matchup["against_bug"] = first ? pkmn.against_bug : (matchup["against_bug"] *= pkmn.against_bug);
                matchup["against_dark"] = first ? pkmn.against_dark : (matchup["against_dark"] *= pkmn.against_dark);
                matchup["against_dragon"] = first ? pkmn.against_dragon : (matchup["against_dragon"] *= pkmn.against_dragon);
                matchup["against_electric"] = first ? pkmn.against_electric : (matchup["against_electric"] *= pkmn.against_electric);
                matchup["against_fairy"] = first ? pkmn.against_fairy : (matchup["against_fairy"] *= pkmn.against_fairy);
                matchup["against_fight"] = first ? pkmn.against_fight : (matchup["against_fight"] *= pkmn.against_fight);
                matchup["against_fire"] = first ? pkmn.against_fire : (matchup["against_fire"] *= pkmn.against_fire);
                matchup["against_flying"] = first ? pkmn.against_flying : (matchup["against_flying"] *= pkmn.against_flying);
                matchup["against_ghost"] = first ? pkmn.against_ghost : (matchup["against_ghost"] *= pkmn.against_ghost);
                matchup["against_grass"] = first ? pkmn.against_grass : (matchup["against_grass"] *= pkmn.against_grass);
                matchup["against_ground"] = first ? pkmn.against_ground : (matchup["against_ground"] *= pkmn.against_ground);
                matchup["against_ice"] = first ? pkmn.against_ice : (matchup["against_ice"] *= pkmn.against_ice);
                matchup["against_normal"] = first ? pkmn.against_normal : (matchup["against_normal"] *= pkmn.against_normal);
                matchup["against_poison"] = first ? pkmn.against_poison : (matchup["against_poison"] *= pkmn.against_poison);
                matchup["against_psychic"] = first ? pkmn.against_psychic : (matchup["against_psychic"] *= pkmn.against_psychic);
                matchup["against_rock"] = first ? pkmn.against_rock : (matchup["against_rock"] *= pkmn.against_rock);
                matchup["against_steel"] = first ? pkmn.against_steel : (matchup["against_steel"] *= pkmn.against_steel);
                matchup["against_water"] = first ? pkmn.against_water : (matchup["against_water"] *= pkmn.against_water);
                if (first)
                    first = false;
            }
            teamMatchupUps[key] = matchup;
        }

        //Return methods for the dictionaries.
        public static List<PokemonModel> getPlayerTeam(string key)
        {
            return playerTeams[key];
        }
        
        public static List<PokemonModel> getPlayerDraft(string key)
        {
            return playerDrafts[key];
        }

        public static Dictionary<string, float> getPlayerMatchup(string key)
        {
            return teamMatchupUps[key];
        }

        //Method for loading the pokemon dataset. 
        public static void LoadPokemon()
        {
            using (var reader = new StreamReader("Pokemon Dataset/Draft Pokemon Dataset.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var Pokemon = csv.GetRecords<PokemonModel>();
                totalDraftDex = Pokemon.ToList();
                totalDraftLoaded = true;
            }
        }
    }
}