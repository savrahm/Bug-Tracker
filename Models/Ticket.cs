using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Ticket
    {
        public Ticket(string title, string description)
        {
            Title = title;
            Description = description;
            Created = DateTime.Now;
        }

        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Closed { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Status { get; set; }
        public FileStream File { get; set;  }
        /*TODO: Figure out how to implement file storing. Try https://www.c-sharpcorner.com/UploadFile/deepak.sharma00/how-to-save-images-in-mysql-database-using-C-Sharp/
         * or https://stackoverflow.com/questions/13047099/read-mediumblob-data-type-from-mysql-in-c-sharp 
         */
    }
}
