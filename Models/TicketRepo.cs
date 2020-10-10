using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repos
{
    public class TicketRepo : IRepo<Ticket>
    {
        List<Ticket> tickets = new List<Ticket>();

        private readonly IDbConnection _conn;

        public TicketRepo(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _conn.Query<Ticket>("SELECT * FROM tickets;");
        }

        public Ticket GetById(int id)
        {
            return _conn.QuerySingle<Ticket>("SELECT * FROM tickets WHERE ticketid = @id;",
                new { id = id });
        }

        public Ticket Insert(Ticket item)
        {
            _conn.Execute("INSERT INTO tickets (title, descripton, priority, created) VALUES(@title, @description, @priority, @created)",
                 new { title = item.Title, description = item.Description, priority = item.Priority, created = item.Created });
            return item;
        }

        public IEnumerable<Ticket> Delete(Ticket item)
        {
            _conn.Execute("DELETE FROM tickets WHERE ticketid = @id;",
                new { id = item.TicketId });
            return GetAll();
        }

        public Ticket Update(Ticket item)
        {
            var current = DateTime.Now;

            _conn.Execute("UPDATE tickets SET ticketid = @ticketid, created = @created, updated = @current, closed = @closed, title = @title, description = @description, priority = @priority, userid = @userid; projectid = @projectid; status = @status, image = @image;",
                new { ticketid = item.TicketId, created = item.Created, updated = item.Updated, closed = item.Closed, title = item.Title, description = item.Description, priority = item.Priority, userid = item.UserId, projectid = item.ProjectId, status = item.Status, image = item.File });

            if (item.Status == "Closed")
            {
                _conn.Execute("UPDATE tickets SET closed = @current;",
                    new { closed = item.Closed });
            }

            return item;
        }

        public IEnumerable<Ticket> Search(string searchTerm)
        {
            return _conn.Query<Ticket>("SELECT * FROM tickets WHERE NAME LIKE @name;",
                new { name = "%" + searchTerm + "%" });
        }

        public void File(Ticket item)
        {
            _conn.Execute("Update tickets SET file = @file WHERE ticketid = @ticketid",
                new { file = item.File, ticketid = item.TicketId });
        }
    }
}
