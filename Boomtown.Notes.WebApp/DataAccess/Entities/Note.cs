using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boomtown.Notes.WebApp.DataAccess.Entities
{
    /// <summary>
    /// Database entity for storing Note information
    /// </summary>
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
    }
}
