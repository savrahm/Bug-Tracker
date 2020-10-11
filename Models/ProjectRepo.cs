using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repos
{
    public class ProjectRepo
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
                new { id = id });
        }

        public Project Insert(Project item)
        {
            _conn.Execute("INSERT INTO projects (title, descripton, priority, created) VALUES(@title, @description, @priority, @created)",
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
            var current = DateTime.Now;

            _conn.Execute("UPDATE projects SET projectid = @projectid, created = @created, updated = @current, closed = @closed, title = @title, description = @description, priority = @priority, userid = @userid; status = @status, file = @file;",
                new { ticketid = item.ProjectId, created = item.Created, updated = item.Updated, closed = item.Closed, title = item.Title, description = item.Description, priority = item.Priority, userid = item.UserId, status = item.Stage, file = item.File });

            if (item.Stage == "Closed")
            {
                _conn.Execute("UPDATE Projects SET closed = @current;",
                    new { closed = item.Closed });
            }

            return item;
        }

        public Project AssignProject()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetProjects()
        {
            throw new NotImplementedException();
        }
    }
}
