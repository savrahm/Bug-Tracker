using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models.Repos
{
    public class ProjectRepo : IRepo<Project>
    {
        public IEnumerable<Project> Delete(Project item)
        {
            throw new NotImplementedException();
        }

        public void File(Project item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        public Project GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Project Insert(Project item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> Search(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Project Update(Project item)
        {
            throw new NotImplementedException();
        }
    }
}
