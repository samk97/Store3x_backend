using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string? buyer_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
    }
}
