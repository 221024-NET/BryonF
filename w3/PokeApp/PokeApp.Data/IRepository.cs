using PokeApp.Logic;

namespace PokeApp.Data
{
    public interface IRepository
    {
        public List<Pokemon> GetAllPokemon();

        public string UpdatePokemonName();
    }
}
