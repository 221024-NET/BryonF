using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Wordle
{
    public class User
    {
        // Fields
        [XmlAttribute]
        public string userName { get; set; }
        public string password { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public double averageTurns { get; set; }
        public int[] turns { get; set; } // turns to win by index

        public XmlSerializer Serializer { get; } = new XmlSerializer(typeof(List<User>));

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

        public string DisplayRecord(string path, List<User> records)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Player \t\t Wins \t\t Losses \t\t Average turns to win");
            foreach (User record in records)
            {
                result.AppendLine($"{record.userName} \t{record.wins}\t\t {record.losses}\t\t {record.averageTurns}");
            }
            return result.ToString();
        }

        public void SerializeAsXml(List<User> records)
        {

            var newStringWriter = new StringWriter();
            Serializer.Serialize(newStringWriter, records);

            File.WriteAllText("./xml", newStringWriter.ToString());

            newStringWriter.Close();
        }

        public List<User> ReadFromXml()
        {
            StreamReader reader = new StreamReader("./xml");
            var records = (List<User>?)Serializer.Deserialize(reader);
            reader.Close();
           
            return records;
        }
    }

}