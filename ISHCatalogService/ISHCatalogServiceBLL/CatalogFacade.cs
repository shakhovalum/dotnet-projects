using ISHCatalogServiceBLL.Services;

namespace ISHCatalogServiceBLL
{
    internal class CatalogFacade
    {
        private readonly ICatalogService _service;

        public CatalogFacade(ICatalogService service)
        {
            _service = service;
        }
    }
}
