using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepo repo;

        public ProjectController(IProjectRepo repo)
        {
            this.repo = repo;
        }

        // GET: ProjectController
        public ActionResult Index()
        {
            var projects = repo.GetAll();
            
            return View(projects);
        }

        // GET: ProjectController/Details/5
        public ActionResult ViewProject(int id)
        {
            var project = repo.GetById(id);

            return View(project);
        }

        // GET: ProjectController/Create
        public IActionResult InsertProject()
        {
            var project = repo.AssignTicketsProp();
            return View(project);
        }

        // POST: ProjectController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public IActionResult InsertProjectToDatabase(Project projectToInsert)
        {
            repo.Insert(projectToInsert);

            return RedirectToAction("Index");
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public IActionResult UpdateProjectToDatabase(Project project)
        {
            repo.Update(project);

            return RedirectToAction("ViewProject", new { id = project.ProjectId });
        }

        public IActionResult DeleteProject(Project project)
        {
            repo.Delete(project);

            return RedirectToAction("Index");
        }

        public IActionResult Search(string searchTerm)
        {
            var search = repo.Search(searchTerm);

            return View(search);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadButtonClick(IFormFile files, Project project)
        {
            throw new NotImplementedException(); 
        }
    }
}
