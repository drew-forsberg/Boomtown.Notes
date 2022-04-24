using System;
using System.Text.Json.Serialization;

namespace Boomtown.Notes.WebApp.Dtos
{
    /// <summary>
    /// Simplified representation of Note information
    /// </summary>
    public class NoteDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("contents")]
        public string Contents { get; set; }

        [JsonPropertyName("created")]
        public DateTimeOffset DateCreated { get; set; }

        [JsonPropertyName("updated")]
        public DateTimeOffset DateUpdated { get; set; }
    }
}
