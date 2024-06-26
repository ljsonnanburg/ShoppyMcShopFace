#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ShoppyMcShopFace.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        // Order Status 0: Shopping cart; User must always have a shopping cart even if empty
        // Order Status 1: Order placed
        // Order Status 2: Order fulfilled
        // Order Status 3: Order returned
        // More to come?
        [Required]
        public int OrderStatus { get; set; }

        public DateTime? DateOrdered { get; set; }
        public DateTime? DateFulfilled { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User? OrderingUser { get; set; }
        public PaymentMethod? PaymentMethodUsed { get; set; }

        public List<ProductInOrder> ProductsInOrder { get; set; } = new();
    }
}
