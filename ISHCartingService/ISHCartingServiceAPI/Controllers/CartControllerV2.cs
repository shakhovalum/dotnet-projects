using Microsoft.AspNetCore.Mvc;
using ISHCartingService.ISHCartingServiceDAL;
using ISHCartingService.ISHCartingServiceModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISHCartingService.Controllers
{
    [Route("v2/cart")]
    [ApiController]
    public class CartControllerV2 : ControllerBase
    {
        private readonly CartingService _cartingService;

        public CartControllerV2(CartingService cartingService)
        {
            _cartingService = cartingService;
        }

        // Get cart items
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartItemsAsync(string cartId)
        {
            var items = await _cartingService.GetCartItemsAsync(cartId);
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // Add item to cart
        [HttpPost("{cartId}/items")]
        public async Task<IActionResult> AddItemToCartAsync(string cartId, [FromBody] CartItem item)
        {
            if (item == null)
            {
                return BadRequest("Item cannot be null");
            }

            await _cartingService.AddItemToCartAsync(cartId, item);
            return Ok();
        }

        // Delete item from cart
        [HttpDelete("{cartId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromCartAsync(string cartId, int itemId)
        {
            await _cartingService.RemoveItemFromCartAsync(cartId, itemId);
            return NoContent();
        }
    }
}