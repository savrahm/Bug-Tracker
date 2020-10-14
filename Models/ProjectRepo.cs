using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repos
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly IDbConnection _conn;

        public ProjectRepo(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Project> Delete(Project item)
        {
            _conn.Execute("DELETE FROM projects WHERE projectid = @id;",
                new { id = item.ProjectId });
            return GetAll();
        }

        public void File(Project item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAll()
        {
            return _conn.Query<Project>("SELECT * FROM projects;");
        }

        public Project GetById(int id)
        {
            return _conn.QuerySingle<Project>("SELECT * FROM projects WHERE projectid = @id;",
                new { id });
        }

        public Project Insert(Project item)
        {
            _conn.Execute("INSERT INTO projects (title, description, priority, created) VALUES(@title, @description, @priority, @created)",
                 new { title = item.Title, description = item.Description, priority = item.Priority, created = item.Created });
            return item;
        }

        public IEnumerable<Project> Search(string searchTerm)
        {
            return _conn.Query<Project>("SELECT * FROM projects WHERE title LIKE @title;",
                new { title = "%" + searchTerm + "%" });
        }

        public Project Update(Project item)
        {
            _conn.Execute("UPDATE projects SET title = @title, description = @description, priority = @priority, userid = @userid; status = @status, file = @file WHERE projectid = @projectid;",
                new { title = item.Title, description = item.Description, priority = item.Priority, userid = item.UserId, status = item.Stage, file = item.File, projectid = item.ProjectId });

            if (item.Stage == "Closed")
            {
                _conn.Execute("UPDATE projects SET closed = CURRENT_TIMESTAMP WHERE projectid = @projectid;",
                    new { closed = item.Closed, projectid = item.ProjectId });
            }

            return item;
        }

        public List<Ticket> GetTickets()
        {
            return _conn.Query<Ticket>("SELECT * FROM tickets;").ToList();
        }

        public Project AssignTicketsProp()
        {
            var ticketList = GetTickets();
            var project = new Project();
            project.Tickets = ticketList;

            return project;
        }
    }
}
