using AspNetCore;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (ticket == null)
            {
                return View("TicketNotFound");
            }

            return View(ticket);
        }
        /*^ is like the Console.ReadLine()--it's getting the data from the user via the View. v is then taking that
        input back to the model and doing something with it*/
        public IActionResult UpdateTicketToDatabase(Ticket ticket)
        {
            repo.UpdateTicket(ticket);

            return RedirectToAction("ViewTicket", new { id = ticket.TicketId });
        }

        public IActionResult InsertTicket()
        {

            return View();
        }

        /*^ is like the Console.ReadLine()--it's getting the data from the user via the View. v is then taking that
        input back to the model and doing something with it*/

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

        public IActionResult Search(string searchTerm)
        {
            var search = repo.SearchTickets(searchTerm);

            return View(search);
        }

        public IActionResult UploadButtonClick(IFormFile files, Ticket ticket)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath =
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")).Root + $@"{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }

                    ticket.File = "/images/" + newFileName;

                    repo.AttachImage(ticket);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
