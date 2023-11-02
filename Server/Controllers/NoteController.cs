using ElevenNoteWebApp_2.Server.Services.Note;
using ElevenNoteWebApp_2.Shared.Models.Note;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElevenNoteWebApp_2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService) 
        {
            _noteService = noteService;
        }

        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null)
                return null;
            return userIdClaim;

        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if(userId == null)
                return false;

            _noteService.SetUserId(userId);
            return true;
        }

        [HttpPost]
        public async Task<List<NoteListItem>> Index()
        {
            if (!SetUserIdInService()) return new List<NoteListItem>();

            var notes = await _noteService.GetAllNotesAsync();
            return notes.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Note(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var note = await _noteService.GetNoteByIdAsync(id);

            if(note == null) return NotFound();

            return Ok(note);
        }

    }
}
