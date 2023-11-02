using ElevenNoteWebApp_2.Server.Data;
using ElevenNoteWebApp_2.Server.Models;
using ElevenNoteWebApp_2.Shared.Models.Note;
using Microsoft.EntityFrameworkCore;

namespace ElevenNoteWebApp_2.Server.Services.Note
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;

        private string _userId;

        public NoteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNoteAsync(NoteCreate model)
        {
            var noteEntity = new Models.Note
            {
                Title = model.Title,
                Content = model.Content,
                OwnerId = _userId,
                CreatedUtc = DateTimeOffset.Now,
                CategoryId = model.CategoryId
                //this was CreatedUtc in the module
            };

            _context.Notes.Add(noteEntity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            var entity = await _context.Notes.FindAsync(noteId);
            if (entity?.OwnerId != _userId)
                return false;

            _context.Notes.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        public Task<bool> DeleteNoteAsync(string userId)
        {
            throw new NotImplementedException();

            //is this done later?
        }

        public async Task<IEnumerable<NoteListItem>> GetAllNotesAsync()
        {
            var noteQuery = _context
                .Notes
                .Where(n => n.OwnerId == _userId)
                .Select(n =>
                    new NoteListItem
                    {
                        Id = n.Id,
                        Title = n.Title,
                        CategoryName = n.Category.Name,
                        CreatedUtc = n.CreatedUtc
                    });
            return await noteQuery.ToListAsync();
        }

        public async Task<NoteDetail> GetNoteByIdAsync(int noteId)
        {
            var noteEntity = await _context
                .Notes
                .Include(nameof(Category))
                .FirstOrDefaultAsync(n => n.Id == noteId && n.OwnerId == _userId);
            if (noteEntity is null)
                return null;

            var detail = new NoteDetail
            {
                Id = noteEntity.Id,
                Title = noteEntity.Title,
                Content = noteEntity.Content,
                CategoryName = noteEntity.Category.Name,
                CreatedUtc = noteEntity.CreatedUtc,
                ModifiedUtc = noteEntity.ModifiedUtc,
                CategoryId = noteEntity.Category.Id,
            };
            return detail;
        }

        public async Task<bool> UpdateNoteAsync(NoteEdit model)
        {
            if (model == null)
                return false;

            var entity = await _context.Notes.FindAsync(model.Id);

            if (entity?.OwnerId != _userId) return false;

            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.CategoryId = model.CategoryId;
            entity.ModifiedUtc = DateTimeOffset.Now;

            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(string userId) =>_userId = userId;
        
    }
}
