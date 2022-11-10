using System.IO;
using System.Runtime.Serialization;
 using System.Runtime.Serialization.Formatters.Binary;

using Wordle;

namespace SaveLoad
{

    public class Serial
    {
        public Serial() { }

        public void saveUser(User user)
        {
            try
            {
                User saving = user;
                string path = "./.users.dat";

                Stream s = File.Open(path, FileMode.Create);
                IFormatter b = new BinaryFormatter();
                b.Serialize(s, saving);
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        public User readUser()
        {
            User loading = new User();
            try
            {


                string path = "./.users.dat";
                Stream s = File.Open(path, FileMode.Open);
                IFormatter b = new BinaryFormatter();
                loading = (User)b.Deserialize(s);
                s.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return loading;

        }
    }
}