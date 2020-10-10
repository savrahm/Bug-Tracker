using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public interface IRepo<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Insert(T item);
        T Update(T item);
        IEnumerable<T> Delete(T item);
        IEnumerable<T> Search(string searchTerm);
        void File(T item);
    }
}
