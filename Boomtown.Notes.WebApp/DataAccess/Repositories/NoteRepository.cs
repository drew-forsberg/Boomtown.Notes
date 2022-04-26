using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boomtown.Notes.WebApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task Create(Note note)
        {
            note.DateCreated = DateTimeOffset.UtcNow;

            _noteContext.Notes.Add(note);

            await _noteContext.SaveChangesAsync();
        }

        public async Task Update(Note note)
        {
            note.DateUpdated = DateTimeOffset.UtcNow;

            _noteContext.Notes.Update(note);

            await _noteContext.SaveChangesAsync();
        }

        public async Task<Note> GetNote(int id)
        {
            var note = await _noteContext.Notes.FindAsync(id);
            return note;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            var notes = await _noteContext.Notes.ToListAsync();
            
            return notes;
        }

        public async Task DeleteNote(Note note)
        {
            _noteContext.Notes.Remove(note);

            await _noteContext.SaveChangesAsync();
        }
    }
}
