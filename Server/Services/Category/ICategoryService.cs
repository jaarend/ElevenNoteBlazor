using ElevenNoteWebApp_2.Shared.Models.Category;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ElevenNoteWebApp_2.Server.Services.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListItem>> GetAllCategoriesAsync();
        Task<bool> CreateCategoryAsync(CategoryCreate model);
        Task<CategoryDetail> GetCategoryByIdAsync(int categoryId);
        Task<bool> UpdateCategoryAsync(CategoryEdit model);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
