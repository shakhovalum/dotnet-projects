﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ISHCartingService.ISHCartingServiceModels
{
    public class CartItem
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAltText { get; set; }
        public required decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        
    }
}
