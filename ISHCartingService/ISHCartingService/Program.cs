using ISHCartingService.ISHCartingServiceDAL;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
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
                Quantity = 46
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

            MongoDBContext context;
            context = new MongoDBContext(connectionString);
            var dataManager = new CartingDataManager(context);
            var service = new CartingService(dataManager);

            await service.AddItemToCartAsync("878788", cartItem1);

            await service.AddItemToCartAsync("878788", cartItem2);

            await service.AddItemToCartAsync("878788", cartItem3);

            await service.RemoveItemFromCartAsync("878788", 778);

            await service.AddItemToCartAsync("878789", cartItem2);

            await service.AddItemToCartAsync("878789", cartItem1);
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
                });

}

