using System;


public class Game
{
    public static string rock = "Rock";
    public static string paper = "Paper";
    public static  string scissors = "Scissors";
    public static string playerChoice= rock;
    public static string compChoice= rock;
    public static string playAgain = "y";
    public static int playerScore = 0;
    public static int tieScore = 0;
    public static int compScore = 0;


    public static bool keepPlaying = true;

    static public void Main(String[] args)
    { 
        while (keepPlaying)
        {
            Console.Clear();
            CLI();
            Console.WriteLine("\n Play Again? y or n");
            playAgain = Console.ReadLine();

            if (playAgain != "y")
            {
                keepPlaying = false;
            }
        }
       
    }

    private static string Shoot()
    {
        Random random = new Random();
        int num = random.Next(1,4);

        switch (num)
        {
            case 1:
                return rock;
                
            case 2:
                return paper;
               
            case 3:
                return scissors;
               
                
            default:
                return "error";

        }
    }

    public static void CLI()
    {
        Console.WriteLine("Rock Paper Scissors\n");
        Console.WriteLine("When you see 'Shoot!' type 'r', 'p', or 's', then press ENTER.\n");
        Console.WriteLine("Player Wins: " + playerScore + " Computer Wins: " + compScore + " Tie :" + tieScore);
        System.Threading.Thread.Sleep(5000);
        Console.WriteLine();
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("ROCK");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("PAPER");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("SCISSORS");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("Shoot!");


        playerChoice= Console.ReadLine();

        if(playerChoice == "r")
        {
            playerChoice = rock;
        }else if(playerChoice == "p")
        {
            playerChoice= paper;
        }else
        {
            playerChoice= scissors;
        }
        compChoice= Shoot();

        checkWin(playerChoice, compChoice);

       

        

    }
   
     /* Three possiblities end game, Tie, Win & Lose */
    private static void checkWin(String player, string comp)
    {
        Console.WriteLine("You chose "+ player + " The computer chose "+comp+"\n");

        
        if(player == comp) //Tie 
        {
            Console.WriteLine("Its a Tie!\n");
            tieScore++;

        }else if(player == rock && comp == scissors || player == paper && comp == rock ||
            player == scissors && comp == paper) // player wins
        {
            Console.WriteLine("You Win!!\n");
            playerScore++;
        }
        else // player Loses
        {
            Console.WriteLine("The Computer Wins!\n");
            compScore++;
        }
    }
    
    
}
