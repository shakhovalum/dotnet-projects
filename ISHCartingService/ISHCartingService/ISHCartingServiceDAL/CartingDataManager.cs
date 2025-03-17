using MongoDB.Driver;
using ISHCartingService.ISHCartingServiceModels;
using MongoDB.Bson;

namespace ISHCartingService.ISHCartingServiceDAL
{
    public class CartingDataManager
    {
        private readonly MongoDBContext _context;

        public CartingDataManager(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            return await _context.Carts.Find(cart => cart.Id == cartId).FirstOrDefaultAsync();
        }

        public async Task AddItemToCartAsync(string cartId, CartItem item)
        {
            var filter = Builders<Cart>.Filter.Eq(c => c.Id, cartId);

            var arrayFilters = new[]
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("i._id", item.Id))
            };

            var update = Builders<Cart>.Update
                .Set("Items.$[i].Name", item.Name)
                .Set("Items.$[i].ImageUrl", item.ImageUrl)
                .Set("Items.$[i].ImageAltText", item.ImageAltText)
                .Set("Items.$[i].Price", item.Price)
                .Set("Items.$[i].Quantity", item.Quantity);

            var updateResult = await _context.Carts.UpdateOneAsync(filter, update, new UpdateOptions { ArrayFilters = arrayFilters });

            if (updateResult.ModifiedCount == 0)
            {
                var pushUpdate = Builders<Cart>.Update.Push(c => c.Items, item);
                await _context.Carts.UpdateOneAsync(filter, pushUpdate);
            }
        }

        public async Task RemoveItemFromCartAsync(string cartId, int itemId)
        {
            var filter = Builders<Cart>.Filter.Eq(c => c.Id, cartId);
            var update = Builders<Cart>.Update.PullFilter(c => c.Items, i => i.Id == itemId);

            await _context.Carts.UpdateOneAsync(filter, update);
        }

        public async Task CreateNewCartAsync(string cartId)
        {
            var cart = new Cart
            {
                Id = cartId,
                Items = new List<CartItem>()
            };

            await _context.Carts.InsertOneAsync(cart);
        }
    }
}
