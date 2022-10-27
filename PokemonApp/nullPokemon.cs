using System;

namespace PokemonApp
{
    public class nullPokemon : Pokemon
    {
        public nullPokemon () : base(" ",0,"None",0, " ")
        {

        }

        public override string ToString()
        {
            return " Empty";
        }
    }
}