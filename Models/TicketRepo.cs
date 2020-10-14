using Dapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
            _conn.Execute("INSERT INTO tickets (title, description, projectid, type, priority, created, userid) VALUES(@title, @description, @projectid, @type, @priority, @created, @userid);",
                 new { title = item.Title, description = item.Description, projectid = item.ProjectId, type = item.Type, priority = item.Priority, created = item.Created, userid = item.UserId });
            return item;
        }

        public IEnumerable<Ticket> Delete(Ticket item)
        {
            _conn.Execute("DELETE FROM tickets WHERE ticketid = @id;",
                new { id = item.TicketId });
            return GetAll();
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
        public void Update(Ticket item)
        {
            _conn.Execute("UPDATE tickets SET title = @title, description = @description, priority = @priority, userid = @userid, projectid = @projectid, stage = @stage, file = @file WHERE ticketid = @ticketid;",
                new { title = item.Title, description = item.Description, priority = item.Priority, userid = item.UserId, projectid = item.ProjectId, stage = item.Stage, file = item.File, ticketid = item.TicketId });

            if (item.Stage == "Closed")
            {
                _conn.Execute("UPDATE tickets SET closed = CURRENT_TIMESTAMP WHERE ticketid = @ticketid;",
                    new { closed = item.Closed, ticketid = item.TicketId });
            }
        }

        public List<Project> GetProjects()
        {
            return _conn.Query<Project>("SELECT * FROM projects;").ToList();
        }

        public Ticket AssignProjectsProp()
        {
            var projectList = GetProjects();
            var ticket = new Ticket();
            ticket.Projects = projectList;

            return ticket;
        }


        /*
         * "Select A Project" OnClick (Project Index View) in modal, ProjectRepo.GetById
         * 
         * 
         * 
         */
    }
}
