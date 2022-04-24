using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boomtown.Notes.WebApp.DataAccess.Entities;

namespace Boomtown.Notes.WebApp.DataAccess.Repositories
{
    /// <summary>
    /// Encapsulates all database logic to access and modify Property entities
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        private readonly NoteContext _noteContext;
        public NoteRepository(NoteContext noteContext)
        {
            _noteContext = noteContext;
        }

        public async Task CreateOrUpdate(Note note)
        {
            var existingNote = await _noteContext.Notes.FindAsync(note.Id);

            if (existingNote == null)
            {
                note.DateCreated = DateTimeOffset.UtcNow;

                _noteContext.Notes.Add(note);
            }
            else
            {
                existingNote.Name = note.Name;
                existingNote.Contents = note.Contents;
                existingNote.DateUpdated = DateTimeOffset.UtcNow;

                _noteContext.Notes.Update(existingNote);
            }

            await _noteContext.SaveChangesAsync();
        }

        public List<Note> GetNotes()
        {
            var notes = _noteContext.Notes.ToList();
            
            return notes;
        }
    }
}
