using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemo.BAL.Interfaces;
using ValidationDemo.DTOs;

namespace ValidationDemo.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            _service.CreateProduct(dto);
            return Ok("Product created successfully");
        }
    }

}
