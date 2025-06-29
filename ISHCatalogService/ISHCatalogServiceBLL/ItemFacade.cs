using ISHCatalogServiceBLL.Services;

namespace ISHCatalogServiceBLL
{
    internal class ItemFacade
    {
        private readonly IItemService _service;

        public ItemFacade(IItemService service)
        {
            _service = service;
        }
    }
}
