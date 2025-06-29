using ISHCatalogServiceBLL.Services;
using ISHCatalogServiceAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISHCatalogServiceBLL.Entities;

namespace ISHCatalogServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Authorize(Policy = "BuyerPolicy")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems([FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _itemService.GetItemsAsync();

            if (categoryId.HasValue)
            {
                items = items.Where(i => i.CategoryId == categoryId.Value);
            }

            var paginatedItems = items.Skip((page - 1) * pageSize).Take(pageSize);

            var itemDtos = paginatedItems.Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Image = i.Image,
                CategoryId = i.CategoryId,
                Price = i.Price,
                Amount = i.Amount
            });

            return Ok(itemDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "BuyerPolicy")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            var itemDto = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Image = item.Image,
                CategoryId = item.CategoryId,
                Price = item.Price,
                Amount = item.Amount
            };

            return Ok(itemDto);
        }

        [HttpPost]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<ActionResult> AddItem(ItemDto itemDto)
        {
            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Image = itemDto.Image,
                CategoryId = itemDto.CategoryId,
                Price = itemDto.Price,
                Amount = itemDto.Amount
            };

            await _itemService.AddItemAsync(item);

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, itemDto);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<ActionResult> UpdateItem(int id, ItemDto itemDto)
        {
            var item = new Item
            {
                Id = id,
                Name = itemDto.Name,
                Description = itemDto.Description,
                Image = itemDto.Image,
                CategoryId = itemDto.CategoryId,
                Price = itemDto.Price,
                Amount = itemDto.Amount
            };

            await _itemService.UpdateItemAsync(item);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteItemAsync(id);

            return NoContent();
        }
    }
}