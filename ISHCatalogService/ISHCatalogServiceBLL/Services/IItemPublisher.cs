using ISHCatalogServiceBLL.Entities;

namespace ISHCatalogServiceBLL.Services
{
    public interface IItemPublisher
    {
        // call this method in facade methods when updating items
        public Task publishItemUpdatedAsync(Item item);
    }
}