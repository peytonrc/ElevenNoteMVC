using ElevenNote.Models;
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
        public ActionResult Index() // ActionResult is a return type; allows us to return a View() method
        {
            var model = new NoteListItem[0]; // Initializing a new instance of the NoteListItem as an IEnumerable with the [0] syntax
            
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
        public ActionResult Create(NoteCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

    }
}