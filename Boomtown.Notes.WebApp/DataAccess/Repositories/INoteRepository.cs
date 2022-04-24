using System.Collections.Generic;
using System.Threading.Tasks;
using Boomtown.Notes.WebApp.DataAccess.Entities;

namespace Boomtown.Notes.WebApp.DataAccess.Repositories
{
    public interface INoteRepository
    {
        Task CreateOrUpdate(Note note);
        List<Note> GetNotes();
    }
}