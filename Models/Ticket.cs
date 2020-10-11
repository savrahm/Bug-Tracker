using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Ticket
    {
        public Ticket()
        {
            Created = DateTime.Now;
        }

        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } = null;
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime? Closed { get; set; } = null;
        public int? UserId { get; set; } = null;
        public int? ProjectId { get; set; } = null;
        public string Stage { get; set; }
        public string File { get; set; } = null;
        /*TODO: Figure out how to implement file storing. Try https://www.c-sharpcorner.com/UploadFile/deepak.sharma00/how-to-save-images-in-mysql-database-using-C-Sharp/
         * or https://stackoverflow.com/questions/13047099/read-mediumblob-data-type-from-mysql-in-c-sharp 
         */
        public IEnumerable<Project> Projects { get; set; }
    }
}
