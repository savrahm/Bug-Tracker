using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repos
{
    public class TicketRepo : ITicketRepo
    {

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
                new { id });
        }

        public Ticket Insert(Ticket item)
        {
            _conn.Execute("INSERT INTO tickets (title, description, priority, created) VALUES(@title, @description, @priority, @created);",
                 new { title = item.Title, description = item.Description, priority = item.Priority, created = item.Created });
            return item;
        }

        public IEnumerable<Ticket> Delete(Ticket item)
        {
            _conn.Execute("DELETE FROM tickets WHERE ticketid = @id;",
                new { id = item.TicketId });
            return GetAll();
        }

        public void Update(Ticket item)
        {


            _conn.Execute("UPDATE tickets SET title = @title, description = @description, priority = @priority, userid = @userid, projectid = @projectid, status = @status, file = @file WHERE ticketid = @ticketid;",
                new { title = item.Title, description = item.Description, priority = item.Priority, userid = item.UserId, projectid = item.ProjectId, status = item.Status, file = item.File, ticketid = item.TicketId });

            //if (item.Status == "Closed")
            //{
            //    _conn.Execute("UPDATE tickets SET closed = @current;",
            //        new { closed = item.Closed });
            //}

           
        }

        public IEnumerable<Ticket> Search(string searchTerm)
        {
            return _conn.Query<Ticket>("SELECT * FROM tickets WHERE title LIKE @title;",
                new { title = "%" + searchTerm + "%" });
        }

        public void File(Ticket item)
        {
            _conn.Execute("Update tickets SET file = @file WHERE ticketid = @ticketid",
                new { file = item.File, ticketid = item.TicketId });
        }

        public IEnumerable<Project> GetOther()
        {
            return _conn.Query<Project>("SELECT title FROM projects;");
        }

        public Ticket AssignOther()
        {
            var projectList = GetOther();
            var ticket = new Ticket();
            ticket.Projects = projectList;

            return ticket;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _conn.Query<Project>("SELECT title FROM projects;");
        }

        public Ticket AssignProject()
        {
            var projectList = GetProjects();
            var ticket = new Ticket();
            ticket.Projects = projectList;

            return ticket;
        }
    }
}
