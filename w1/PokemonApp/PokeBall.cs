using System;

namespace PokemonApp
{

    public class PokeBall
    {

        bool isEmpty {get; set;}
        double catchRate{get; set;}

        Pokemon caught {get; set;}

        public PokeBall()
        {
            isEmpty = true;
            catchRate = .5;
            caught = new nullPokemon();

        

        }

        public void catchemAll (Pokemon pokemon)
        {
            caught = pokemon;
            isEmpty = false;

        }

        public override string ToString()
        {
            return  caught.ToString();
        }


    }
}