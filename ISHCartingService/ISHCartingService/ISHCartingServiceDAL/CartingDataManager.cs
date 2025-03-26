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
            var cartItemFromDb = FindCartItemByCartIdAndItemId(cartId, item.Id);

            if (cartItemFromDb == null || !AreCartItemsEqual(cartItemFromDb, item))
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

        private CartItem FindCartItemByCartIdAndItemId(string cartId, int cartItemId)
        {
            var filter = Builders<Cart>.Filter.Eq(c => c.Id, cartId);
            var cart = _context.Carts.Find(filter).FirstOrDefault();

            return cart?.Items.FirstOrDefault(item => item.Id == cartItemId);
        }

        private bool AreCartItemsEqual(CartItem item1, CartItem item2)
        {
            return item1.Id == item2.Id &&
                   item1.Name == item2.Name &&
                   item1.ImageUrl == item2.ImageUrl &&
                   item1.ImageAltText == item2.ImageAltText &&
                   item1.Price == item2.Price &&
                   item1.Quantity == item2.Quantity;
        }
    }
}
