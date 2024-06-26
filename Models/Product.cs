using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ShoppyMcShopFace.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        // 0 = Currently for sale
        // 1 = In system, not publicly listed
        // 2
        // 3 = Not yet staged, handled by TempProductController
        [Required]
        public int ProductStatus { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name { get; set; }

        // [Required]
        [MinLength(2, ErrorMessage = "Remember to come back to this and make an actual URL validator")]
        public string? ImageURL { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Negative prices would allow users to use you for an infinite money glitch")]
        public float Price { get; set; } = 0;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Can't have fewer than 0 items, what are you doing")]
        public int Stock { get; set; } = 0;

        [Required]
        [MinLength(2, ErrorMessage = "Brand name must be at least 2 characters")]
        public string? Brand { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Category name must be at least 2 characters")]
        public string? Category { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Product description must be at least 10 characters")]
        public string? Description { get; set; }

        [Column(TypeName = "jsonb")]
        public string? TagsJSON { get; set; }

        //foreign key
        public int CreatorId { get; set; }

        public User? CreatingUser { get; set; }
        
        [NotMapped]
        public Dictionary<string, List<string>>? Tags { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public void SerializeTags()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            this.TagsJSON = JsonSerializer.Serialize(this.Tags, options);
        }
    }


}
