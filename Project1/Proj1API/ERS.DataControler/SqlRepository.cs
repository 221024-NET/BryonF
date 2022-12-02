﻿using ERS.Model;
using System.Data.SqlClient;

namespace ERS.DataControler
{
    public class SqlRepository : IRepository
    {
        public string? _connectionString { set; get; }

        public SqlRepository() { }
        public SqlRepository(string connection)

        {
            this._connectionString = connection ??
                        throw new ArgumentNullException(nameof(connection));

        }

        //-----------------------------Ticket Table---------------------------------\\

        /// <summary>
        /// Returns all tickets in the database in a List.
        /// </summary>
        /// <returns>
        /// List of type Ticket
        /// </returns>
        public IEnumerable<Ticket> getAllTickets(string _con)
        {

            Ticket t;
            List<Ticket> result = new List<Ticket>();

            using SqlConnection connection = new SqlConnection(_con);
            connection.Open();
            using SqlCommand cmd = new SqlCommand("SELECT Ticket_ID, About_Data, Amount," +
                                                   "Ticket_Status, Ticket_Type ,FK_UserId FROM ERS.Tickets", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                t = new Ticket();
                t.id = reader.GetInt32(0);
                t.data = reader.GetString(1);
                t.amount = (double)reader.GetDecimal(2);
                t.status = (TicketStatus)reader.GetInt32(3);
                t.type = (TicketType)reader.GetInt32(4);
                t.employee = reader.GetInt32(5);

                result.Add(t);
            }
            connection.Close();

            return result;
        }

        /// <summary>
        /// Returns a List of all Tickets with the status of Pending
        /// </summary>
        /// <param name="_con">
        /// Connection String to database
        /// </param>
        /// <returns>
        /// List obj of all tickets with status pending
        /// </returns>
        public IEnumerable<Ticket> GetPendingTickets(string _con)
        {
            Ticket t;

            List<Ticket> tckts = new List<Ticket>();

            using SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd = "SELECT Ticket_ID, About_Data, Amount, Ticket_Status,Ticket_Type, FK_UserId " +
                "FROM ERS.Tickets WHERE Ticket_Status = 0";

            using SqlCommand command = new SqlCommand(cmd, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                t = new Ticket();
                t.id = reader.GetInt32(0);
                t.data = reader.GetString(1);
                t.amount = (double)reader.GetDecimal(2);
                t.status = (TicketStatus)reader.GetInt32(3);
                t.type = (TicketType)reader.GetInt32(4);
                t.employee = reader.GetInt32(5);

                tckts.Add(t);
            }


            return tckts;

        }

        /// <summary>
        /// Returns a List<T> of Type Ticket that were generated by
        /// a specific user.
        /// </summary>
        /// <param name="id">
        ///  The User_ID of the User
        /// </param>
        /// <returns>
        /// List Type Ticket generated by a single user
        /// </returns>
        public IEnumerable<Ticket> GetMyTickets(int id, string _con)
        {
            List<Ticket> result = new List<Ticket>();

            Ticket t;
            using SqlConnection connection = new SqlConnection(_con);
            connection.Open();


            using SqlCommand cmd = new SqlCommand("SELECT Ticket_ID, About_Data, Amount," +
                                                   "Ticket_Status,Ticket_Type,FK_UserId FROM ERS.Tickets" +
                                                   " WHERE FK_UserId = @id", connection);

            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                t = new Ticket();
                t.id = reader.GetInt32(0);
                t.data = reader.GetString(1);
                t.amount = (double)reader.GetDecimal(2);
                t.status = (TicketStatus)reader.GetInt32(3);
                t.type = (TicketType)reader.GetInt32(4);
                t.employee = reader.GetInt32(5);

                result.Add(t);
            }
            connection.Close();


            return result;
        }

        /// <summary>
        /// Returns a list of tickets from a specific user, with a specific type
        /// </summary>
        /// <param name="id"> Employee ID number of the user</param>
        /// <param name="type">The type (Food, Travel, Lodging, Other) cast as int of tickets requested </param>
        /// <param name="_con"> Connection string to database</param>
        /// <returns></returns>
        public IEnumerable<Ticket> GetMyTicketsByType(int id, int type, string _con)
        {
            List<Ticket> result = new List<Ticket>();

            Ticket t;
            using SqlConnection connection = new SqlConnection(_con);
            connection.Open();


            using SqlCommand cmd = new SqlCommand("SELECT Ticket_ID, About_Data, Amount," +
                                                   "Ticket_Status,Ticket_Type,FK_UserId FROM ERS.Tickets" +
                                                   " WHERE FK_UserId = @id AND  Ticket_Type = @type" , connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@type", type);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                t = new Ticket();
                t.id = reader.GetInt32(0);
                t.data = reader.GetString(1);
                t.amount = (double)reader.GetDecimal(2);
                t.status = (TicketStatus)reader.GetInt32(3);
                t.type = (TicketType)reader.GetInt32(4);
                t.employee = reader.GetInt32(5);

                result.Add(t);
            }
            connection.Close();

            return result;
        }

        /// <summary>
        /// Retrieves a specific ticket from the database
        /// </summary>
        /// <param name="id">
        ///  The Ticket_ID of the ticket requested
        /// </param>
        /// <returns>
        ///  A Ticket Object
        /// </returns>
        public Ticket GetTicketById(int id, string _con)
        {
            Ticket t = new Ticket();
            using SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd = "SELECT Ticket_ID, Amount, About_Data, Ticket_Status, Ticket_Type, FK_UserId " +
                " FROM ERS.Tickets WHERE Ticket_ID = @id";
            using SqlCommand command = new SqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();
            // Ticket(int id, string data, double amount, int employee, TicketStatus status)
            while (reader.Read())
            {
                t = new Ticket(reader.GetInt32(0), reader.GetString(2),
                   (double)reader.GetDecimal(1), reader.GetInt32(5),
                   (TicketStatus)reader.GetInt32(3), (TicketType)reader.GetInt32(4));
            }
            connection.Close();
            return t;
        }

        /// <summary>
        /// Changes the status of a ticket, 
        /// Only available to Manager objects,
        /// and tickets with the status of pending
        /// </summary>
        /// <param name="u">
        /// The User requesting the status change
        /// </param>
        /// <param name="status">
        ///  The new status of the Ticket object
        /// </param>
        /// <returns>
        /// Returns True if  status change is sucessful.
        /// Returns False if Ticket status is not Pending (cannot change status if already approved or denied)
        /// </returns>
        public bool ChangeTicketStatus(Ticket t, TicketStatus status, string _con)
        {

            if (t.status == TicketStatus.Pending)
            {
                using SqlConnection connection = new SqlConnection(_con);
                connection.Open();

                string cmd = "UPDATE ERS.Tickets " +
                    "SET Ticket_Status = @status" +
                    " WHERE Ticket_ID = @id";
                using SqlCommand command = new SqlCommand(cmd, connection);

                command.Parameters.AddWithValue("@status", (int)status);
                command.Parameters.AddWithValue("@id", t.id);

                command.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Submits a new Ticket object to the database
        /// </summary>
        /// <param name="t">
        ///  The Ticket object added to the database
        /// </param>
        /// <param name="Id">
        ///  The User_ID of the employee that created the Ticket object.
        /// </param>

        public void AddNewTicket( Ticket t, string _con)
        {
            using SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmdText = "INSERT INTO ERS.Tickets (About_Data, Amount, " +
                "Ticket_Status,Ticket_Type ,FK_UserId)" +
                " VALUES (@data, @amount, @status, @type, @id) ";

            using SqlCommand command = new SqlCommand(cmdText, connection);

            command.Parameters.AddWithValue("@data", t.data);
            command.Parameters.AddWithValue("@amount", t.amount);
            command.Parameters.AddWithValue("@status", (int)t.status);
            command.Parameters.AddWithValue("@type", (int)t.type);
            command.Parameters.AddWithValue("@id",t.employee);

            command.ExecuteNonQuery();

            connection.Close();
        }

        //------------------------------Employees Table------------------------\\

        /// <summary>
        /// Returns a List of Users currently in the database
        /// </summary>
        /// <returns>
        /// List of users
        /// </returns>
        public IEnumerable<User> GetAllUsers(string _con)
        {
            List<User> users = new List<User>();
            User u;// new User();

            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd = "SELECT User_ID, User_Name, User_Email, User_pswd "
                           + " FROM ERS.Employees";

            SqlCommand command = new SqlCommand(cmd, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                u = new User();
                u.empId = reader.GetInt32(0);
                u.userName = reader.GetString(1);
                u.email = reader.GetString(2);
                u.password = reader.GetString(3);


                users.Add(u);
            }
            reader.Close();

            return users;
        }
        /// <summary>
        /// Attemps to match a temporary User object to an existing user in the database
        /// by checking the  User email and password fields
        /// </summary>
        /// <param name="temp"> 
        /// Temporary User object attempting to log in to the system
        /// </param>
        /// <param name="_con">
        /// connection string to the database
        /// </param>
        /// <returns>
        /// a User object if the temporary User matches an existing one,
        /// NULL if it dose not.
        /// </returns>
        public User EmployeeLogIn(User temp, string _con)
        {


            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd = "SELECT User_ID, User_Name, User_Email, User_pswd " +
                "FROM ERS.Employees " +
                "WHERE User_Email = @email AND User_pswd = @password";

            SqlCommand command = new SqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@email", temp.email);
            command.Parameters.AddWithValue("@password", temp.password);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = new User();
                temp.empId = reader.GetInt32(0);
                temp.userName = reader.GetString(1);
                temp.email = reader.GetString(2);
                temp.password = reader.GetString(3);


            }
            if (temp.userName == "")
            {
                return null;
            }
            else
            {
                return temp;
            }

        }
        /// <summary>
        /// Returns a specific User
        /// </summary>
        /// <param name="id">
        /// The User employee ID num
        /// </param>
        /// <returns>
        /// The User with the corrisponding employee ID
        /// </returns>
        public User GetUserById(int id, string _con)
        {
            User u = new User();
            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd = "SELECT User_ID, User_Name, User_Email, User_pswd, User_Manager"
                           + " FROM ERS.Employees " +
                           "WHERE User_ID = @id";

            SqlCommand command = new SqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                u.empId = reader.GetInt32(0);
                u.userName = reader.GetString(1);
                u.email = reader.GetString(2);
                u.password = reader.GetString(3);

            }
            connection.Close();

            return u;
        }

        /// <summary>
        /// Adds a new user to the database. Then retrieves the correct ID number
        /// of the user object, and assigns it to the object.
        /// </summary>
        /// <param name="u"> 
        /// The new User object
        /// </param>
        /// <returns> 
        /// Newly created User object with correct ID
        /// </returns>
        public User CreateNewUser(User u, string _con)
        {

            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            //add User to database
            string cmd1 = "INSERT INTO ERS.Employees (User_Name, User_pswd, User_Email)" +
                "VALUES (@name, @pswd, @email)";

            SqlCommand command = new SqlCommand(cmd1, connection);

            command.Parameters.AddWithValue("@name", u.userName);
            command.Parameters.AddWithValue("@pswd", u.password);
            command.Parameters.AddWithValue("@email", u.email);


            command.ExecuteNonQuery();

            //get Users correct ID from auto generated Database; assign id to correct field

            string cmd2 = "SELECT User_ID FROM ERS.Employees " +
                          "WHERE User_Email = @email";
            command = new SqlCommand(cmd2, connection);
            command.Parameters.AddWithValue("@email", u.email);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                u.empId = reader.GetInt32(0);
            }

            connection.Close();
            return u;
        }

        //-----------------------------------Managers Table-----------------------------------\\

        /// <summary>
        /// Attemps to match a temporary Manager object to an existing manager in the database
        /// by checking the email and password fields
        /// </summary>
        /// <param name="temp">
        /// Temporary Manager object with only email and password fields 
        /// </param>
        /// <param name="_con">
        /// Connection string to the database
        /// </param>
        /// <returns>
        ///  A complete Mager object if both fields match,
        ///  or null if not found
        /// </returns>
        public Manager ManagerLogIn(Manager temp, string _con)
        {
            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd = "SELECT Man_ID, Man_Name, Man_Email, Man_pswd  " +
                "FROM ERS.Managers " +
                "WHERE Man_Email = @email AND Man_pswd = @password";

            SqlCommand command = new SqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@email", temp.email);
            command.Parameters.AddWithValue("@password", temp.password);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = new Manager();
                temp.empId = reader.GetInt32(0);
                temp.userName = reader.GetString(1);
                temp.email = reader.GetString(2);
                temp.password = reader.GetString(3);


            }
            if (temp.userName == "")
            {
                return null;
            }
            else
            {
                return temp;
            }
        }

        /// <summary>
        /// Return a List of all Mangers in the Managers Table
        /// </summary>
        /// <param name="_con">
        /// Connection string to the database
        /// </param>
        /// <returns>
        /// List container of all managers
        /// </returns>

        public IEnumerable<Manager> GetAllManagers(string _con)
        {
            List<Manager> managers = new List<Manager>();
            Manager m;

            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd = "SELECT User_ID, User_Name, User_Email, User_pswd "
                           + " FROM ERS.Managers";

            SqlCommand command = new SqlCommand(cmd, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                m = new Manager();
                m.empId = reader.GetInt32(0);
                m.userName = reader.GetString(1);
                m.email = reader.GetString(2);
                m.password = reader.GetString(3);


                managers.Add(m);
            }
            reader.Close();

            return managers;
        }


        /// <summary>
        /// Adds a new manager to the manager's table
        /// </summary>
        /// <param name="m">
        /// The new manager Object to be added
        /// </param>
        /// <param name="_con">
        /// conection string to the database
        /// </param>
        /// <returns>
        /// A complete manager object
        /// </returns>
        public Manager CreateNewManager(Manager m, string _con)
        {

            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            //add User to database
            string cmd1 = "INSERT INTO ERS.Managers (Man_Name, Man_Email, Man_pswd)" +
                "VALUES (@name, @pswd, @email)";

            SqlCommand command = new SqlCommand(cmd1, connection);

            command.Parameters.AddWithValue("@name", m.userName);
            command.Parameters.AddWithValue("@pswd", m.password);
            command.Parameters.AddWithValue("@email", m.email);


            command.ExecuteNonQuery();

            //get Users correct ID from auto generated Database; assign id to correct field

            string cmd2 = "SELECT User_ID FROM ERS.Employees " +
                          "WHERE User_Email = @email";
            command = new SqlCommand(cmd2, connection);
            command.Parameters.AddWithValue("@email", m.email);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                m.empId = reader.GetInt32(0);
            }

            connection.Close();
            return m;
        }

        /// <summary>
        /// Promotes an existing Employee to Manager, By first
        ///  adding the new Manger to the manager table,
        ///  
        ///  
        ///  NOTE: The promoted Employee's employee ID will change,
        ///  the Employee will still exist in the employees table
        ///   to avoid conficts with The Ticket Table
        /// </summary>
        /// <param name="u">
        ///  The user object being promoted to Manager
        /// </param>
        /// <param name="_con"></param>
        /// <returns></returns>
        public bool PromoteEmployee (User u, string _con)
        {
            Manager manager = new Manager();
            manager.userName = u.userName;
            manager.password = u.password;
            manager.email = u.email;    

            SqlConnection connection = new SqlConnection(_con);
            connection.Open();

            string cmd1 = "INSERT INTO ERS.Managers (Man_Name, Man_Email, Man_pswd) " +
                "VALUES (@name, @pswd, @email);";
            SqlCommand command = new SqlCommand(cmd1, connection);

            command.Parameters.AddWithValue("@name", manager.userName);
            command.Parameters.AddWithValue("@email", manager.email);
            command.Parameters.AddWithValue("pswd", manager.password);

            command.ExecuteNonQuery();

            

          // string cmd2 = "DELETE FROM ERS.Employees WHERE User_ID = @id";

            //command = new SqlCommand(cmd2, connection);

            //command.Parameters.AddWithValue("@id", u.empId);

            //command.ExecuteNonQuery();

            return true;
        }
    }



}