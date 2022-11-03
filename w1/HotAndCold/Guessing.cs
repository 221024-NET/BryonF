namespace HotAndCold
{
    public class Guessing
    {
        // Fields
        
        // Constructors
        public Guessing(){}

        // Methods

        /// <summary>
        /// This method generates a value from a random number generator and returns that value.
        /// </summary>
        /// <returns> int secretNum </returns>
        public int GenerateSecretNumber()
        {
            var rand = new Random(); // generate a random

            int secretNum = rand.Next(101); // pick a random number between 0 and 100

            return secretNum;
        }

        /// <summary>
        /// This method accepts a users guess as a string, converts to an in, and returns that value.
        /// </summary>
        /// <returns>int userNum</returns>
        public int GetUserNumber()
        {
            int userNum = -1;

            Console.WriteLine("Enter a guess for the secret number: "); // prompt the user for a guess

            string userChoice = Console.ReadLine(); // accept the users guess

            while(!Int32.TryParse(userChoice, out userNum))// convert from string to int so we can compare, if possible
            {
                Console.WriteLine("Please enter only numerical values."); // and prompting the user for an alternative if it's not
                userChoice = Console.ReadLine();
            } 

            return userNum;

        }

        /// <summary>
        /// This method compares the user guess to the secret number, and print out the appropriate response.
        /// </summary>
        /// <param name="secretNum"></param>
        /// <param name="userNum"></param>
        /// <returns></returns>
        public string PrintResult( int secretNum, int userNum)
        {
            if (secretNum == userNum) // if the user guessed correctly
            {
                return "Congatulations, you've guessed the secret number!"; // congratulate them!
//                loop = false;
//                break;
//                continue; // NOT a good answer in this case!
//                return; // valid, but not teh best option right now.
            }

            else if (secretNum > userNum) // if user guesses low
            {
                return "Oops, you've guessed too low!";
            }

            else // if user guesses too high
            {
                return "Oops, too high!";
            }   
        }   
    }
}