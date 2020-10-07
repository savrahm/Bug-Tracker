using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public interface ITicketRepo
    {
        public IEnumerable<Ticket> GetAllTickets();
        public Ticket GetTicket(int id);
        public void UpdateTicket(Ticket ticket);
        public void InsertTicket(Ticket ticketToCreate);
        public void DeleteTicket(Ticket ticket);
    }
}
