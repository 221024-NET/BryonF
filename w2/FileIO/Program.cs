using System.IO;

namespace FileIO
{
    public class Program
    {
        public static void Main()
        {

            string path = "./testFile.txt";

            string[] text ={"Hi","Hellow", "There", "How's", "it", "Going"};

           // File.WriteAllLines(path, text);

          // File.AppendAllLines(path, text);

          string[] content =File.ReadAllLines(path);

          foreach(string s in content)
          {
            Console.WriteLine(s);
          }
  

        }
    }
}
