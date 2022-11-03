using System;
using PokemonApp;

namespace Program{

    class Program{

        static void Main(string[] args)
        {
            
           PokeBall ball = new PokeBall();
            Pikachu pika = new Pikachu();
            Console.WriteLine (pika.ToString());//calls Pokemon toString()
            Console.WriteLine(ball.ToString());

            ball.catchemAll(pika);
            Console.WriteLine(ball.ToString());



            

        }
        

    }

}