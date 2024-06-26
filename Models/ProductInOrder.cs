#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Add this using statement to access NotMapped
using System.ComponentModel.DataAnnotations.Schema;
namespace ShoppyMcShopFace.Models;
public class ProductInOrder
{
    [Key]
    public int ProductInOrderId { get;set; }

    [Required]
    public int Quantity {get; set;}
    
    // foreign key
    public int OrderId {get; set;}
    public int ProductId {get; set;}

    //nav props
    public Order? OrderBelongedTo {get; set;}
    public Product? OrderedProduct {get; set;}
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}