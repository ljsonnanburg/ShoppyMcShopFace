#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ShoppyMcShopFace.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [UniqueEmail]
        public string Email { get; set; }

        [Required]
        [Range(1, 10)]
        public int UserLevel { get; set; }

        public string? ShippingAddress { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        public Order ShoppingCart { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Order> OrdersUserPlaced { get; set; } = new();
        public List<Invoice> InvoicesOfUser { get; set; } = new();

        public User()
        {
            ShoppingCart = new Order
            {
                UserId = this.UserId,
                OrderStatus = 0
            };
        }
    }

    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Email is required!");
            }

            MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
            if (_context.Users.Any(e => e.Email == value.ToString()))
            {
                return new ValidationResult("Email must be unique!");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
