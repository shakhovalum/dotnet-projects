using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ISHCartingService.ISHCartingServiceModels
{
    public class Cart
    {
        [BsonId]
        public required string Id { get; set; }

        public List<CartItem> Items { get; set; }
    }
}
