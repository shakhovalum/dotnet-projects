using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISHCartingService.ISHCartingServiceModels;

namespace ISHCartingService.ISHCartingServiceDAL
{
    public class CartingService
    {

        private CartingDataManager _manager;

        public CartingService(CartingDataManager manager)
        {
            _manager = manager;
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            return await _manager.GetCartAsync(cartId);
        }

        public async Task AddItemToCartAsync(string cartId, CartItem item)
        {
            var cart = await _manager.GetCartAsync(cartId);

            if (cart == null) 
            {
                await   _manager.CreateNewCartAsync(cartId);
            }

            await _manager.AddItemToCartAsync(cartId, item);
        }

        public async Task RemoveItemFromCartAsync(string cartId, int itemId)
        {
            await _manager.RemoveItemFromCartAsync(cartId, itemId);
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string cartId)
        {
            var cart = await _manager.GetCartAsync(cartId);
            return cart?.Items ?? new List<CartItem>();
        }
    }
}
