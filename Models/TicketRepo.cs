using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class TicketRepo : ITicketRepo
    {
        private readonly IDbConnection _conn;

        public TicketRepo(IDbConnection conn)
        {
            _conn = conn;
        }
        public void DeleteTicket(Ticket ticket)
        {
            _conn.Execute("DELETE FROM tickets WHERE ticket_id = @id;",
                new { id = ticket.TicketId });
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _conn.Query<Ticket>("SELECT * FROM tickets;");
        }

        public Ticket GetTicket(int id)
        {
            return _conn.QuerySingle<Ticket>("SELECT * FROM tickets WHERE ticket_id = @id;",
                new { id = id});
        }

        public void InsertTicket(Ticket ticketToCreate)
        {
            _conn.Execute("INSERT INTO tickets (title, descripton, priority, created) VALUES(@title, @description, @priority, @created)",
                new { title = ticketToCreate.Title, description = ticketToCreate.Description, priority = ticketToCreate.Priority, created = DateTime.Now });
        }

        public void UpdateTicket(Ticket ticket)
        {
            _conn.Execute("UPDATE tickets SET title = @title, description = @description, priority = @priority, updated = @updated status = @status;",
                new {title = ticket.Title, description = ticket.Description, priority = ticket.Priority, updated = DateTime.Now, status = ticket.Status });
        }

        public IEnumerable<Ticket> SearchTickets(string searchTerm)
        {
            return _conn.Query<Ticket>("SELECT * FROM tickets WHERE NAME LIKE @name;",
                new { name = "%" + searchTerm + "%" });
        }

        public void AttachImage(Ticket ticket)
        {
            _conn.Execute("Update tickets SET Image = @image WHERE ticket_id = @ticket_id",
                new { image = ticket.Image, ticket_id = ticket.TicketId });
        }
    }
}
