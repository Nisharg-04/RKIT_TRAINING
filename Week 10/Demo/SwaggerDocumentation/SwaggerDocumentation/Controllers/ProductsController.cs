using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SwaggerDocumentation.Models;

namespace SwaggerDocumentation.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <remarks>
        /// Returns the complete list of products from cache or database.
        /// </remarks>
        /// <returns>List of products</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult GetAll()
        {
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200, IsAvailable = true },
            new Product { Id = 2, Name = "Phone", Price = 800, IsAvailable = true }
        };

            return Ok(products);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns>Product details</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product id");

            return Ok(new Product
            {
                Id = id,
                Name = "Laptop",
                Price = 1200,
                IsAvailable = true
            });
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="request">Product creation payload</param>
        /// <returns>Created product</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Create(ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Id = 3,
                Name = request.Name,
                Price = request.Price,
                IsAvailable = true
            };

            return Created("api/products/3", product);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns>No content</returns>
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }
    }
}