using ISHCatalogServiceBLL.Entities;

namespace ISHCatalogServiceBLL.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item?> GetItemByIdAsync(int id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
    }
}