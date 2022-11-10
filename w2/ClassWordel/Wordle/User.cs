using System.Text;
using System.IO;

namespace Wordle
{
    [Serializable]
    public class User 
    {
        // Fields
        string userName;
        string password;
        int wins;
        int losses;
        double averageTurns;
        int[] turns; // turns to win by index

        // Costructors
        public User() { }

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            this.wins = 0;
            this.losses = 0;
            this.averageTurns = 0;
            this.turns = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        }

        public User(string userName, string password, int wins, int losses, double averageTurns, int[] turns)
        {
            this.userName = userName;
            this.password = password;
            this.wins = wins;
            this.losses = losses;
            this.averageTurns = averageTurns;
            this.turns = turns;
        }

        // Methods
        public void UpdateRecord(bool win, int turn)
        {
            if (win)
            {
                this.wins++;
                this.turns[turn]++;
                this.averageTurns = FindAverage(turn);
            }

            else
                this.losses++;
        }

        public double FindAverage(int turn)
        {
            int sum = 0;
            int count = 0;

            for (int i = 1; i <= 6; i++)
            {
                sum += (turns[i] * i);
                count += turns[i];
            }
            return sum / count;
        }

        public string DisplayRecord(string path)
        {
            string[] records = File.ReadAllLines(path);
            StringBuilder result = new StringBuilder();

            result.AppendLine("Player \t\t Wins \t\t Losses \t\t Average turns to win");
            foreach (string record in records)
            {
                string[] current = record.Split(", ");

                result.AppendLine($"{current[0]} \t{current[2]}\t\t {current[3]}\t\t {current[4]}");

                User currentUser = new User(current[0], current[1]);
                //result.AppendLine(record);
                //result.AppendLine($"{userName} \t{this.wins}\t\t {this.losses}\t\t {this.averageTurns}");
            }
            return result.ToString();

            // string result = "this string";
            // result = result + "some other string being concatenated";
            // return result;

            //return "this string";
        }

        public void SaveRecord(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.userName}, {this.password}, {this.wins}, {this.losses}, {this.averageTurns}, {this.turns[1]}, {this.turns[2]}, {this.turns[3]}, {this.turns[4]}, {this.turns[5]}, {this.turns[6]}");
            File.AppendAllText(path, sb.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(userName + " "+ password+" "+ wins+" "+ losses+ " "+averageTurns);
            return sb.ToString();
        }
    }

}