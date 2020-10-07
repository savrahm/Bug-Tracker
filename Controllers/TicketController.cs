using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepo repo;

        public TicketController(ITicketRepo repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var tickets = repo.GetAllTickets();

            return View(tickets);
        }

        public IActionResult ViewTicket(int id)
        {
            var ticket = repo.GetTicket(id);

            return View(ticket);
        }

        public IActionResult UpdateTicket(int id)
        {
            Ticket ticket = repo.GetTicket(id);

            if(ticket == null)
            {
                return View("TicketNotFound");
            }

            return View(ticket);
        }
        //Why are these 2 separate?
        public IActionResult UpdateTicketToDatabase(Ticket ticket)
        {
            repo.UpdateTicket(ticket);

            return RedirectToAction("ViewTicket", new { id = ticket.TicketId });
        }

        public IActionResult NewTicket()
        {
           /*Ask Whit about this, it's not how they did it for Product in that earlier ASP.NET MVC project, or I may 
            * have done it wrong there. Also I changed the Ticket constructor to require Title and Description when
            * a new Ticket is created, since title and description aren't nullable in the table? Is that right?
            */
            var ticket = new Ticket();
            repo.InsertTicket(ticket);
                    
            return View(ticket);
        }
        /*What is InsertTicketToDatabase here for? It looks like it's doing what NewTicket is doing ^ and then immediately 
         * taking you to a view of ALL the tickets--why?? Won't the "New Ticket" method in the TicketRepo update the 
         * database with the new ticket? It's executing an INSERT INTO. Can I just have InsertTicket and NewTicket without
         * InsertTicketToDatabase? 
        */
        public IActionResult InsertTicketToDatabase(Ticket ticketToInsert)
        {
            repo.InsertTicket(ticketToInsert);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTicket(Ticket ticket)
        {
            repo.DeleteTicket(ticket);

            return RedirectToAction("Index");
        }
    }
}
