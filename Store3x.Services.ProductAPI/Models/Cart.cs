﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store3x.Services.ProductAPI.Models
{
    [Table("product_shoppingcart")]
    public class Cart
    {
        [Key, Column(Order = 0)]
        public string? buyer_id { get; set; }

        [Key, Column(Order = 1)]
        public int product_id { get; set; }

        public int quantity { get; set; }
    }
}
