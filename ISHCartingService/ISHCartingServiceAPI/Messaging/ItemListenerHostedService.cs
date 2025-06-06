namespace ISHCartingServiceAPI.Messaging
{
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class ItemListenerHostedService : IHostedService
    {
        private readonly ItemListener _itemListener;

        public ItemListenerHostedService(ItemListener itemListener)
        {
            _itemListener = itemListener;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _itemListener.StartProcessingAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _itemListener.StopProcessingAsync();
        }
    }
}
