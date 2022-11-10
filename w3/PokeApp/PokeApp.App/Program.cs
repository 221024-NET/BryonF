using PokeApp.Data;
using PokeApp.Logic;

namespace PokeApp.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

           

            

            string ConnectionString = File.ReadAllText(@"/Revature/221024-NET/ConnectionStrings/PokeAppConnectionString.txt");

            IRepository repo = new SqlRepository(ConnectionString);

            Pokemon myPokemon = new Pokemon("Jimmy", 1, 0, 10, 4, 00, 153);

            List<Pokemon> Pokemons = repo.GetAllPokemon();
            Console.WriteLine(myPokemon.Speak());

            foreach (Pokemon poke in Pokemons)
                Console.WriteLine(poke.Speak());





        }
    }
}