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
        public async Task<IEnumerable<NoteDto>> GetAsync()
        {
            try
            {
                // Use the Note repository to get notes from database
                var notes = await _noteRepository.GetNotesAsync();

                // Convert the results to a simplified format for display purposes
                var displayNotes = notes.Select(note => _mapper.Map<NoteDto>(note));

                return displayNotes;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve notes from database");
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<NoteDto> GetAsync(int id)
        {
            try
            {
                // Use the Note repository to get notes from database
                var note = await _noteRepository.GetNote(id);

                // Convert the results to a simplified format for display purposes
                var displayNote = _mapper.Map<NoteDto>(note);

                return displayNote;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Could not retrieve note (ID = ${id}) from database");
                throw;
            }
        }

        [HttpPost]
        public async Task<StatusCodeResult> PostAsync([FromBody] NoteDto noteDto)
        {
            if (noteDto == null)
            {
                return BadRequest();
            }

            try
            {
                // Convert the note DTO to the DB entity
                var noteEntity = _mapper.Map<Note>(noteDto);

                // Create the note in the DB
                await _noteRepository.Create(noteEntity);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Note ID = {noteDto.Id} could not be saved to the database");
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutAsync(int id, [FromBody] NoteDto noteDto)
        {
            if (noteDto == null)
            {
                return BadRequest();
            }

            try
            {
                // Convert the note DTO to the DB entity
                var noteEntity = _mapper.Map<Note>(noteDto);

                // Update the note in the DB
                await _noteRepository.Update(noteEntity);

                return Ok ();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Note ID = {noteDto.Id} could not be saved to the database");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteAsync(int id)
        {
            try
            {
                var note = await _noteRepository.GetNote(id);

                if (note == null)
                {
                    return NotFound();
                }

                // Update the note in the DB
                await _noteRepository.DeleteNote(note);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Note (ID = {id}) could not be deleted from the database");
                throw;
            }
        }
    }
}
