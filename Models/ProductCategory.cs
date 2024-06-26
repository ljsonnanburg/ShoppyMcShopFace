// #pragma warning disable CS8618
// using System.ComponentModel.DataAnnotations;
// // Add this using statement to access NotMapped
// using System.ComponentModel.DataAnnotations.Schema;
// namespace ShoppyMcShopFace.Models;
// public class ProductCategory
// {        
//     [Key]        
//     public int ProductCategoryId { get; set; }
    
//     [Required]
//     [MinLength(2, ErrorMessage = "Category name must be at least 2 characters")]
//     public string Name { get; set; }
    
//     public List<Product> ProductsInCategory { get;set; } = new();
//     // public List<Tag> CategoriesAssociatedWithTag { get;set; } = new();
    
//     public DateTime CreatedAt {get;set;} = DateTime.UtcNow;       
//     public DateTime UpdatedAt {get;set;} = DateTime.UtcNow;
// }

