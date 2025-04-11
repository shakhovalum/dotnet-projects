using ISHCatalogServiceBLL.Entities;

namespace ISHCatalogServiceBLL.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}