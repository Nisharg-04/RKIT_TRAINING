using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductsMinimalAPIWithVaidation.Models
{
    public class Product
    {

       
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }

        [Required, Range(0, 10)]
        public int Quantity { get; set; }
    }
}
