using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISHCatalogServiceDAL.Entities;

namespace ISHCatalogServiceBLL.Services
{
    public interface IItemService
    {
        IEnumerable<Item> GetItems();
        Item GetItemById(int id);
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);
    }
}