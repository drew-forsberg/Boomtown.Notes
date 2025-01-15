using AutoMapper;
using Boomtown.Notes.WebApp.DataAccess.Entities;
using Boomtown.Notes.WebApp.DataAccess.Repositories;
using Boomtown.Notes.WebApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boomtown.Notes.WebApp.Controllers
{
    //TODO: Add guard clause/validation for all action method inputs. Use DataAnnotation attributes on NoteDto to simplify validation.

    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NotesController(ILogger<NotesController> logger, INoteRepository noteRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                // Use the Note repository to get notes from database
                var notes = await _noteRepository.GetNotesAsync();

                // Convert the results to a simplified format for display purposes
                var displayNotes = notes.Select(note => _mapper.Map<NoteDto>(note));

                return Ok(displayNotes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve notes from database");
                throw;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                // Use the Note repository to get notes from database
                var note = await _noteRepository.GetNote(id);

                // Confirm that the requested ID exists
                if (note == null)
                {
                    return NotFound();
                }

                // Convert the results to a simplified format for display purposes
                var displayNote = _mapper.Map<NoteDto>(note);

                return Ok(displayNote);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve note (ID = ${id}) from database", id);
                throw;
            }
        }

        //TODO: Could use a simplified DTO here since only the Name and Contents fields are required/expected when adding a new note
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] NoteDto noteDto)
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
                _logger.LogError(e, "Note could not be added to the database");
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] NoteDto noteDto)
        {
            if (noteDto == null)
            {
                return BadRequest();
            }

            try
            {
                // Confirm that the requested ID exists
                var note = await _noteRepository.GetNote(id);

                if (note == null)
                {
                    return NotFound();
                }

                note.Name = noteDto.Name;
                note.Contents = noteDto.Contents;

                // Convert the note DTO to the DB entity
                var noteEntity = _mapper.Map<Note>(note);

                // Update the note in the DB
                await _noteRepository.Update(noteEntity);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Note ID = {id} could not be updated in the database", id);
                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                // Confirm that the requested ID exists
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
                _logger.LogError(e, "Note (ID = {id}) could not be deleted from the database", id);
                throw;
            }
        }
    }
}
