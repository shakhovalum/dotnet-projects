using ISHCatalogServiceBLL.Services;
using ISHCatalogServiceAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using ISHCatalogServiceBLL.Entities;

namespace ISHCatalogServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CategoriesController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _catalogService.GetCategoriesAsync();

            var categoryDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image,
                ParentCategoryId = c.ParentCategoryId
            });

            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _catalogService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                ParentCategoryId = category.ParentCategoryId
            };

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Image = categoryDto.Image,
                ParentCategoryId = categoryDto.ParentCategoryId
            };

            await _catalogService.AddCategoryAsync(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = new Category
            {
                Id = id,
                Name = categoryDto.Name,
                Image = categoryDto.Image,
                ParentCategoryId = categoryDto.ParentCategoryId
            };

            await _catalogService.UpdateCategoryAsync(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _catalogService.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}