#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ShoppyMcShopFace.Models;

public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public int? UserId { get; set; } 

    public DateTime InvoiceDate  {get; set;} = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // navprops
    public User? InvoicedUser { get; set; }
    public List<InvoiceLineItems> ItemsInInvoice { get; set; } = new();
}