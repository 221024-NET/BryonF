
using System.Text;

namespace ERS.Model
{

    public class Ticket
    {


        public int id { get; set; }
        public string data { get; set; }
        public double amount { get; set; }
        public int employee { get; set; }

        public TicketStatus status { get; set; }
        public TicketType type { get; set; }

        public string requestData { get; set; }
        public int requestInt { get; set; }




        public Ticket() { }

        public Ticket(int empId, double amt, string data, TicketType type)
        {
            employee = empId;
            amount = amt;
            this.data = data;
            status = TicketStatus.Pending;
            this.type = type;
        }

        public Ticket(int id, string data, double amount, int employee, TicketStatus status, TicketType type)
        {
            this.id = id;
            this.data = data;
            this.amount = amount;
            this.employee = employee;
            this.status = status;
            this.type = type;
        }
        public Ticket(int employee, double amount, string data, int status)
        {
            this.employee = employee;
            this.amount = amount;
            this.data = data;
            this.status = (TicketStatus)status;


        }


        public void changeStatus(TicketStatus s)
        {

            status = s;
        }






        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("ID: " + id + " Created by: " + employee +" Type: "+type + " Amount: " + amount +
             " For: " + data + " Status: " + status);

            return sb.ToString();
        }


    }
}