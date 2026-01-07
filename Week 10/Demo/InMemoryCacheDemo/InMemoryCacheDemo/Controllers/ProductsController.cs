using InMemoryCacheDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace InMemoryCacheDemo.Controllers
{
   
    [RoutePrefix("api")]
    public class ProductsController : ApiController
    {
        /// <summary>
        /// Get cached products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [Route("cacheget")]
       
        public IHttpActionResult GetCachedData()
        {
            var data = ProductCacheService.GetProducts();
            return Ok(data);
        }

        [HttpGet]
        [Route("removeProductCache")]
        public IHttpActionResult removeProducts() { 
            ProductCacheService.RemoveProducts();
            return Ok();
        }

        [HttpGet]
        [Route("clearCache")]
        public IHttpActionResult clearCache()
        {
            ProductCacheService.Clear();
            return Ok();
        } 
    }
}