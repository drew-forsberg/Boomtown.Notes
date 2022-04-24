using AutoMapper;
using Boomtown.Notes.WebApp.DataAccess.Entities;
using Boomtown.Notes.WebApp.Dtos;

namespace Boomtown.Notes.WebApp.Mapping
{
    /// <summary>
    /// Creates automapping between the Note DB entity and DTO
    /// </summary>
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<NoteDto, Note>();
        }
    }
}
