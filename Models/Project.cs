using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Project
    {
        public Project()
        {
            Created = DateTime.Now;
        }

        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Closed { get; set; } //TODO: try to get this one working https://www.mysqltutorial.org/mysql-if-statement/
        public int UserId { get; set; }
        public string Stage { get; set; }
        public FileStream File { get; set; }
    }
}
