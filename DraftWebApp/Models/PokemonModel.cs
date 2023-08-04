using CsvHelper.Configuration.Attributes;

namespace DraftWebApp.Models
{
    public class PokemonModel
    {
        [Index(0)]
        public string abilities { get; set; }
        [Index(1)]
        public float against_bug { get; set; }
        [Index(2)]
        public float against_dark { get; set; }
        [Index(3)]
        public float against_dragon { get; set; }
        [Index(4)]
        public float against_electric { get; set; }
        [Index(5)]
        public float against_fairy { get; set; }
        [Index(6)]
        public float against_fight { get; set; }
        [Index(7)]
        public float against_fire { get; set; }
        [Index(8)]
        public float against_flying { get; set; }
        [Index(9)]
        public float against_ghost { get; set; }
        [Index(10)]
        public float against_grass { get; set; }
        [Index(11)]
        public float against_ground { get; set; }
        [Index(12)]
        public float against_ice { get; set; }
        [Index(13)]
        public float against_normal { get; set; } 
        [Index(14)]
        public float against_poison { get; set; }
        [Index(15)]
        public float against_psychic { get; set; }
        [Index(16)]
        public float against_rock { get; set; }
        [Index(17)]
        public float against_steel { get; set; }
        [Index(18)]
        public float against_water { get; set; }
        [Index(19)]
        public int attack { get; set; }
        [Index(20)]
        public int base_total { get; set; }
        [Index(21)]
        public int defense { get; set; }
        [Index(22)]
        public int hp { get; set; }
        [Index(23)]
        public string name { get; set; }
        [Index(24)]
        public int pokedex_number { get; set; }
        [Index(25)]
        public int sp_attack { get; set; }
        [Index(26)]
        public int sp_defense { get; set; }
        [Index(27)]
        public int speed { get; set; }
        [Index(28)]
        public string type1 { get; set; }
        [Index(29)]
        public string type2 { get; set; }
        [Index(30)]
        public string tier { get; set; }
        [Index(31)]
        public string medialink { get; set; }
        [Index(32)]
        public string sitelink { get; set; }

    }
}
