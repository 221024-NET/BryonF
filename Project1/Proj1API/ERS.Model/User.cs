using System.Text;

namespace ERS.Model
{
    public class User
    {

        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }


        public int empId { get; set; }

        public string requestData { get; set; }
        public int requestInt { get; set; }




        public User()
        { }

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;

            this.userName = "";
        }
        public User(string name, string email, string password, int empId)
        {
            this.userName = name;
            this.email = email;
            this.password = password;
            this.empId = empId;

        }

        public User(string name, string email, string password, bool manager)
        {
            this.userName = name;
            this.email = email;
            this.password = password;

        }
        public User(string name, string email, string password)
        {
            this.email = email;
            this.userName = name;
            this.password = password;

        }



       





        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(empId + " ");
            sb.Append(userName + " ");
            sb.Append(email + " ");

            return sb.ToString();

        }

    }
}