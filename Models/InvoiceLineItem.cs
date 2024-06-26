#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ShoppyMcShopFace.Models;

public class InvoiceLineItems
{
    [Key]
    public int LineItemId { get; set; }

    // Foreign Keys
    public int? InvoiceId { get; set; } 
    public int? ProductId { get; set; } 

    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // navprops
    
}