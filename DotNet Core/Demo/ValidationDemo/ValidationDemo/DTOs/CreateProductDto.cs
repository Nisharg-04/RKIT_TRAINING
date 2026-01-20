using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ValidationDemo.DTOs
{

    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100)]
        [DefaultValue("Accounting Software")]
        public string Name { get; set; }

        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000")]
        [DefaultValue(100)]
        public decimal Price { get; set; }

        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        [DefaultValue(1)]

        public int Quantity { get; set; }
    }

}
