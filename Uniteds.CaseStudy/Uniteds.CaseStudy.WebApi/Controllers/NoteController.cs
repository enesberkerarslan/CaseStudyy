using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Uniteds.CaseStudy.Domain.Interfaces;
using Uniteds.CaseStudy.Domain.Models;
using AutoMapper;
using Uniteds.CaseStudy.Domain.DTOs;
using Uniteds.CaseStudy.Domain.Mapping;

namespace Uniteds.CaseStudy.API.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteController(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = _noteRepository.GetAllNotes();
            var noteDtos = _mapper.Map<IEnumerable<NoteDto>>(notes);
            return Ok(noteDtos);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // İlgili id'ye sahip notu veritabanından silme işlemini gerçekleştirin
                _noteRepository.DeleteNote(id);

                return Ok("Note deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] NoteDto updatedNote)
        {
            var existingNote = _noteRepository.GetNoteById(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            // Gerekli güncellemeleri yapın
            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.Content = updatedNote.Content;

            _noteRepository.UpdateNote(existingNote);

            return Ok("Update is succes");
        }

        [HttpPost]
        public IActionResult CreateNote([FromBody] NoteDto noteDto)
        {
            noteDto.IsDeleted = false;
            var noteModel = _mapper.Map<Note>(noteDto);
            // Yeni notu veritabanına eklemek için gerekli işlemleri yapın
            _noteRepository.AddNote(noteModel);

            return Ok("Created a succes note.");
        }
    }

    
}
