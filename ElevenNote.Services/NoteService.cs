﻿using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId; // Private field of type Guid called "_userId"

        public NoteService(Guid userId) // Constructor
        {
            _userId = userId;
        }

        // Create Note Method
        public bool CreateNote(NoteCreate model)
        {
            Note entity = new Note() // entity is the name given to the new Note instance
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
                NoteType = model.NoteType,
                CategoryId = model.CategoryId,
                CreatedUtc = DateTimeOffset.Now
            };

            using (ApplicationDbContext ctx = new ApplicationDbContext()) // ctx is the name given to the new ApplicationDbContext instance
            {
                ctx.Notes.Add(entity); // Notes is from Dbcontext and we are adding to the Note entity
                return ctx.SaveChanges() == 1;
            }
        }

        // Get Notes Method : See all notes from a specific user
        public IEnumerable<NoteListItem> GetNotes()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(e =>
                    new NoteListItem
                    {
                        NoteId = e.NoteId,
                        Title = e.Title,
                        NoteType = e.NoteType,
                        Category = e.CategoryId + " " + e.Category.Name,
                        IsStarred = e.IsStarred,
                        CreatedUtc = e.CreatedUtc
                    });
                return query.ToArray();
            }
        }

        // Get Note Detail By Id
        public NoteDetail GetNoteById(int id)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Note entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == id && e.OwnerId == _userId);
                return
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        Content = entity.Content,
                        Category = entity.CategoryId + " " + entity.Category.Name,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        // Edit/Update Note By Id
        public bool UpdateNote(NoteEdit model)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Note entity =
                    ctx
                    .Notes
                    .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.CategoryId = model.CategoryId;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.IsStarred = model.IsStarred;

                return ctx.SaveChanges() == 1;
            }
        }

        // Delete Note By Id
        public bool DeleteNote(int noteId)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Note entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                ctx.Notes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
