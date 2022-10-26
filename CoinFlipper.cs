using System;

public class Program
{
	// Fields
	// Constructor
	// Methods
	public static void Main()
	{
		Console.WriteLine("Starting Coin Flipper:");
		
		string UserNumber = Console.ReadLine();
		int Num = Int32.Parse(UserNumber);
		
		Console.WriteLine(Num);
		
		var rand = new Random();
		
		for (int i = 0; i < Num; i++)
		{
			int coin = rand.Next(2);
			
			Console.WriteLine(coin);

			if (coin == 0)
			{
				Console.WriteLine("Heads");	
			}
			else
			{
				Console.WriteLine("Tails");
			}
		}
	}
}