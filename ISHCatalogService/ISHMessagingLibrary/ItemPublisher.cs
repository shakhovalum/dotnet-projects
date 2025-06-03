using Azure.Messaging.ServiceBus;
using ISHCatalogServiceBLL.Entities;
using ISHCatalogServiceBLL.Services;
using System.Text.Json;
using System.Threading.Tasks;

public class ItemPublisher : IItemPublisher
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;

    public ItemPublisher(string connectionString = 
        "Replacement", 
        string topicName = "item-changes-v1")
    {
        _client = new ServiceBusClient(connectionString);
        _sender = _client.CreateSender(topicName);
    }

    public async Task publishItemUpdatedAsync(Item item)
    {
        var messageBody = JsonSerializer.Serialize(item);
        var message = new ServiceBusMessage(messageBody)
        {
            Subject = "ItemChange",
            SessionId = item.Id.ToString() + DateTime.UtcNow, 
            ApplicationProperties =
            {
                { "Id", item.Id }
            }
        };

        await _sender.SendMessageAsync(message);
    }
}