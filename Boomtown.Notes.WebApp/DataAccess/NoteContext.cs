using Boomtown.Notes.WebApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boomtown.Notes.WebApp.DataAccess
{
    /// <summary>
    /// Entity Framework DbContext for accessing the database
    /// </summary>
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
    }
}
