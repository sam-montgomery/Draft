namespace DraftWebApp.Models
{
    public class TeamBuilderModel
    {
        public List<PokemonModel> team { get; set; }
        public bool full;


        public TeamBuilderModel() 
        { 
            this.team = new List<PokemonModel>();
            full = false;
        }   
    }
}
