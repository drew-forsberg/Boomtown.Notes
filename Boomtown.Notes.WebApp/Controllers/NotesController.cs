using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Boomtown.Notes.WebApp.DataAccess.Entities;
using Boomtown.Notes.WebApp.DataAccess.Repositories;
using Boomtown.Notes.WebApp.Dtos;

namespace Boomtown.Notes.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NotesController(ILogger<NotesController> logger, INoteRepository noteRepository, IMapper mapper)
        {
            _logger = logger;
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<NoteDto> Get()
        {
            try
            {
                // Use the Note repository to get notes from database
                var notes = _noteRepository.GetNotes();

                // Convert the results to a simplified format for display purposes
                // The conversion is too complex for AutoMapper
                var displayNotes = notes.Select(note => _mapper.Map<NoteDto>(note));

                return displayNotes;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve notes from database");
                throw;
            }
        }

        [HttpPost]
        public async Task Post(NoteDto noteDto)
        {
            if (noteDto == null)
            {
                throw new ArgumentNullException(nameof(noteDto));
            }

            try
            {
                // Convert the note DTO to the DB entity
                var noteEntity = _mapper.Map<Note>(noteDto);

                // Create or update the property, depending on whether it has already been saved by the user
                await _noteRepository.CreateOrUpdate(noteEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Note ID = {noteDto.Id} could not be saved to the DB");
                throw;
            }
        }
    }
}
