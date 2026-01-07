using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerDocumentation.Models
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product
    {
        /// <summary>Product unique identifier</summary>
        public int Id { get; set; }

        /// <summary>Name of the product</summary>
        public string Name { get; set; }

        /// <summary>Product price</summary>
        public decimal Price { get; set; }

        /// <summary>Is product available</summary>
        public bool IsAvailable { get; set; }
    }
}