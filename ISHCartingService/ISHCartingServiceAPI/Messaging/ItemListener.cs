namespace ISHCartingServiceAPI.Messaging
{
    using Azure.Messaging.ServiceBus;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ItemListener
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;

        public ItemListener(string connectionString =
            "ENDPOINT",
            string topicName = "item-changes-v1",
            string subscriptionName = "item-updates-sbscr")
        {
            _client = new ServiceBusClient(connectionString);
            _processor = _client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            });

            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;
        }

        public async Task StartProcessingAsync()
        {
            await _processor.StartProcessingAsync();
        }

        public async Task StopProcessingAsync()
        {
            await _processor.StopProcessingAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Item item = JsonSerializer.Deserialize<Item>(body);

            Console.WriteLine($"Received item: {item.Name}, Id: {item.Id}");

            await args.CompleteMessageAsync(args.Message);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Message handler encountered an exception {args.Exception}.");
            Console.WriteLine($"- ErrorSource: {args.ErrorSource}");
            Console.WriteLine($"- Entity Path: {args.EntityPath}");
            Console.WriteLine($"- FullyQualifiedNamespace: {args.FullyQualifiedNamespace}");

            return Task.CompletedTask;
        }
    }
}
