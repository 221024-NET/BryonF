using System;

namespace CollectionsExamples.App
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Collection Example Starting!"); 
            Timer myTimer = new Timer();
             new StackAndQueue();
            TimeSpan runTime = myTimer.Run();
            Console.WriteLine("Total elapsed time is: {0} ms", runTime.TotalMilliseconds);
        }
    }
}