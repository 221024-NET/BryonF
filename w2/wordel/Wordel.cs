namespace wordel
{
    public class Wordel
    {
       List<string> wordList;
        string randWord;
        public Wordel(List<string> words)
        {
            this.wordList = new List<string>( words);
            randWord = randomWord(wordList);

        }

        private string randomWord(List<string> wordList)
        {

           string[] words =  wordList.ToArray();

           Random rand = new Random();
           int i = rand.Next(0,words.Length);
            
            return words[i];

        }

        public string getRandWord()
        {
             return randWord;
        }
    }



}