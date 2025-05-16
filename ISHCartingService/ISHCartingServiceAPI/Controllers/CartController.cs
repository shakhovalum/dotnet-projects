using Microsoft.AspNetCore.Mvc;
using ISHCartingService.ISHCartingServiceDAL;
using ISHCartingService.ISHCartingServiceModels;
using System.Threading.Tasks;

namespace ISHCartingService.Controllers
{
    [Route("v1/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartingService _cartingService;

        public CartController(CartingService cartingService)
        {
            _cartingService = cartingService;
        }

        // Get cart info
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartAsync(string cartId)
        {
            var cart = await _cartingService.GetCartAsync(cartId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
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