using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

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
            var tickets = repo.GetAll();

            return View(tickets);
        }

        public IActionResult ViewTicket(int id)
        {
            var ticket = repo.GetById(id);

            return View(ticket);
        }

        public IActionResult UpdateTicket(int id)
        {
            Ticket ticket = repo.GetById(id);

            if (ticket == null)
            {
                return View("TicketNotFound");
            }

            return View(ticket);
        }
        
        public IActionResult UpdateTicketToDatabase(Ticket ticket)
        {
            repo.Update(ticket);

            return RedirectToAction("ViewTicket", new { id = ticket.TicketId });
        }

        public IActionResult InsertTicket()
        {
            var ticket = repo.AssignProject();
            return View(ticket);
        }

        public IActionResult InsertTicketToDatabase(Ticket ticketToInsert)
        {
            repo.Insert(ticketToInsert);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTicket(Ticket ticket)
        {
            repo.Delete(ticket);

            return RedirectToAction("Index");
        }

        public IActionResult Search(string searchTerm)
        {
            var search = repo.Search(searchTerm);

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
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files")).Root + $@"{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }

                    ticket.File = "/files/" + newFileName;

                    repo.File(ticket);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
