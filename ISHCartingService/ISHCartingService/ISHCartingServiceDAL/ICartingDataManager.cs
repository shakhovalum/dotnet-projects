using ISHCartingService.ISHCartingServiceModels;

namespace ISHCartingService.ISHCartingServiceDAL
{
    public interface ICartingDataManager
    {
        Task<Cart> GetCartAsync(string cartId);
        Task AddItemToCartAsync(string cartId, CartItem item);
        Task RemoveItemFromCartAsync(string cartId, int itemId);
        Task CreateNewCartAsync(string cartId);
    }
}