using System.Collections.Generic;
using System.Threading.Tasks;
using Boomtown.Notes.WebApp.DataAccess.Entities;

namespace Boomtown.Notes.WebApp.DataAccess.Repositories
{
    public interface INoteRepository
    {
        Task Create(Note note);
        Task Update(Note note);
        Task<List<Note>> GetNotesAsync();
        Task<Note> GetNote(int id);
        Task DeleteNote(Note note);
    }
}