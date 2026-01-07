using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerDocumentation.Models
{
    /// <summary>
    /// Request model for creating a product
    /// </summary>
    public class ProductCreateRequest
    {
        /// <summary>Product name</summary>
        public string Name { get; set; }

        /// <summary>Product price</summary>
        public decimal Price { get; set; }
    }
}