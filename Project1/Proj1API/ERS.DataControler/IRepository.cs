using ERS.Model;

namespace ERS.DataControler
{
    public interface IRepository
    {
        public IEnumerable<Ticket> getAllTickets(string _con);
        public IEnumerable<Ticket> GetMyTickets(int id, string _con);
        public Ticket GetTicketById(int id, string _con);
        //public void AddNewTicket(User u, Ticket t, string _con);
        public bool ChangeTicketStatus(Ticket t, TicketStatus status, string _con);


        public IEnumerable<User> GetAllUsers(string _con);
        public User GetUserById(int id, string _con);
        public User CreateNewUser(User u, string _con);




    }
}
