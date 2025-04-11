using ISHCatalogServiceBLL.Services;
using ISHCatalogServiceBLL.Entities;
using ISHCatalogServiceDAL;
using Microsoft.Extensions.DependencyInjection;

namespace ISHCatalogServiceApp
{
    class CatalogServiceRun
    {
        public static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<CatalogContext>()
                .AddScoped<ICatalogService, CatalogService>()
                .AddScoped<IItemService, ItemService>()
                .BuildServiceProvider();

            var catalogService = serviceProvider.GetService<ICatalogService>();
            var itemService = serviceProvider.GetService<IItemService>();

            var newCategory = new Category
            {
                Name = "Cat01",
                Image = "http://Cat01.jpg"
            };

            await catalogService.AddCategoryAsync(newCategory);

            var categories = await catalogService.GetCategoriesAsync();
            foreach (var category in categories)
            {
                Console.WriteLine($"Category: {category.Name}, Image: {category.Image}");
            }

            var newItem = new Item
            {
                Name = "Cat2",
                Description = "Desc2",
                Image = "http://Cat2.jpg",
                CategoryId = newCategory.Id,
                Price = 100,
                Amount = 50
            };

            await itemService.AddItemAsync(newItem);

            var items = await itemService.GetItemsAsync(); 
            foreach (var item in items)
            {
                Console.WriteLine($"Item: {item.Name}, Category: {item.CategoryId}");
            }
        }
    }
}