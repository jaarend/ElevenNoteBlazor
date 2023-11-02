using ElevenNoteWebApp_2.Server.Data;
using ElevenNoteWebApp_2.Shared.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace ElevenNoteWebApp_2.Server.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> CreateCategoryAsync(CategoryCreate model)
        {
            if (model == null)
                return false;
            var categoryEntity = new Models.Category
            {
                Name = model.Name
            };

           _context.Categories.Add(categoryEntity);
            return await _context.SaveChangesAsync() == 1;
        }


        public async Task<IEnumerable<CategoryListItem>> GetAllCategoriesAsync()
        {
            var categoryQuery =
                _context
                .Categories
                .Select(entity =>
                    new CategoryListItem
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                    });

            return await categoryQuery.ToListAsync();
        }

        public Task<CategoryDetail> GetCategoryByIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(CategoryEdit model)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
