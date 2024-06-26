// https://fabric.inc/blog/commerce/ecommerce-database-design-example
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ShoppyMcShopFace.Models;

public class PaymentMethod
{
    [Key]
    public int PaymentMethodId { get; set; }

    public int? UserId { get; set; } 

    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string MethodNickname { get; set; }

    [Required]
    public string PaymentType { get; set; }

    [Required]
    public string Provider { get; set; }

    [Required]
    public int AccountNumber {get;set;}

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    //navprops
    public User? PaymentMethodOwner { get; set; }
    public List<Order> OrdersPaidFor { get;set; } = new();
}

