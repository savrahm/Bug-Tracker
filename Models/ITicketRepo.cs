using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public interface ITicketRepo
    {
        IEnumerable<Ticket> GetAll();
        Ticket GetById(int id);
        Ticket Insert(Ticket ticket);
        void Update(Ticket ticket);
        IEnumerable<Ticket> Delete(Ticket ticket);
        IEnumerable<Ticket> Search(string searchTerm);
        void File(Ticket ticket);
        IEnumerable<Project> GetProjects();
        Ticket AssignProject();
    }
}
