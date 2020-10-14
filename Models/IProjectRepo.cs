using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public interface IProjectRepo
    {
        IEnumerable<Project> Delete(Project item);
        void File(Project item);
        IEnumerable<Project> GetAll();
        Project GetById(int id);
        Project Insert(Project item);
        IEnumerable<Project> Search(string searchTerm);
        Project Update(Project item);
        public List<Ticket> GetTickets();
        public Project AssignTicketsProp();
    }
}
