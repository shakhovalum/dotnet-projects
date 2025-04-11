using ISHCartingService.ISHCartingServiceDAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISHCartingService.ISHCartingServiceModels;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();
            var config = host.Services.GetRequiredService<IConfiguration>();
            var connectionString = config.GetSection("ConnectionString:MongoDB").Value;
            Console.WriteLine($"MongoDB Connection String: {connectionString}");

            var cartItem1 = new CartItem
            {
                Id = 777,
                Name = "Item 777",
                ImageUrl = "http://dummy.com/item777.jpg",
                ImageAltText = "Image 777",
                Price = 1000000,
                Quantity = 49
            };

            var cartItem2 = new CartItem
            {
                Id = 777,
                Name = "Item 777 new val",
                ImageUrl = "http://dummy.com/item777.jpg",
                ImageAltText = "Image 777_new_02",
                Price = 100440000,
                Quantity = 498
            };

            var cartItem3 = new CartItem
            {
                Id = 778,
                Name = "Item 778",
                ImageUrl = "http://dummy.com/item778.jpg",
                ImageAltText = "Image 778",
                Price = 1770000,
                Quantity = 99
            };

            var context = new MongoDBContext(connectionString);
            var dataManager = new CartingDataManager(context);
            var service = new CartingService(dataManager);

            await service.AddItemToCartAsync("1001", cartItem1);
            await service.AddItemToCartAsync("1001", cartItem2);
            await service.AddItemToCartAsync("1001", cartItem3);
            await service.RemoveItemFromCartAsync("1001", 778);
            await service.AddItemToCartAsync("1002", cartItem2);
            await service.AddItemToCartAsync("1002", cartItem1);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<MongoDBContext>(sp =>
                {
                    var configuration = sp.GetRequiredService<IConfiguration>();
                    var connectionString = configuration.GetSection("ConnectionString:MongoDB").Value;
                    return new MongoDBContext(connectionString);
                });

                services.AddTransient<ICartingDataManager, CartingDataManager>();
                services.AddTransient<CartingService>();
            });
}