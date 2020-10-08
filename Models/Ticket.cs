using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class Ticket
    {
        public Ticket()
        {

        }

        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Closed { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public string Status { get; set; }
        public string Image { get; set;  }
    }
}
