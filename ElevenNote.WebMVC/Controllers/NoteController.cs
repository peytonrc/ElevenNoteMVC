using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize] // Views are accessible only to logged in users
    public class NoteController : Controller // Path: localhost:xxxx/Note
    {
        // GET: Note
        public ActionResult Index() // ActionResult is a return type; allows us to return a View() method : Displays all notes for current user
        {
           // var model = new NoteListItem[0]; ---> Initializing a new instance of the NoteListItem as an IEnumerable with the [0] syntax

            Guid userId = Guid.Parse(User.Identity.GetUserId());
            NoteService service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model); // Returns a view for the above path: "...Note/Index"
        }

        // GET: Note/Create
        public ActionResult Create() // Method that GETS the create VIEW
        {
            return View();
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model) // Makes sure the model is valid, grabs the userId, calls on CreateNote model, and returns the user back to the index view
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created."; // Tempdata removes info after it's accessed
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        // GET: Note/Details
        public ActionResult Details(int id)
        {
            NoteService svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }













        // Helper Method
        private NoteService CreateNoteService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            NoteService service = new NoteService(userId);
            return service;
        }
    }
}