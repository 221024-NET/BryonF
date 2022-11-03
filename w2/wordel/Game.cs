namespace wordel
{
    public class Game
    {
        Wordel wordel;
        public Game()
        {
            List<string> words = new List<string>();

            string[] word = File.ReadAllLines("./words.txt");
            foreach (string s in word)
            {
                words.Add(s);
            }

            wordel = new Wordel(words);
            startGame();
        }

        private void startGame()
        {
            bool playing = true;
            while (playing)
            {
                Console.WriteLine("enter a 5 letter word:");
              //  Console.WriteLine("cheat to test methods: "+ wordel.getRandWord());

                string guess = Console.ReadLine();
                Console.WriteLine("you guessed " + guess);

                if (guess == wordel.getRandWord())
                {
                    Console.WriteLine("Correct! the word is: " + wordel.getRandWord());
                    playing = false;
                }
                else
                {
                    string correctLetters="";
                    char[] g =guess.ToCharArray();
                    char[] w = wordel.getRandWord().ToCharArray();

                    for(int i =0; i< g.Length-1;i++)
                    {
                        for(int j=0; j< w.Length-1;j++)
                        {
                            if (g[i] == w[j])
                            {
                               correctLetters += g[i].ToString();
                               continue;
                            }
                        }

                    }

                    Console.WriteLine("the correct letters in your guess are: "+ correctLetters);
                    

                }
            }


        }





    }
}