using RedisAndMySqlDemo.Service;
using System.Web.Http;

[RoutePrefix("api/products")]
public class ProductsController : ApiController
{
    private readonly ProductService _service;

    public ProductsController()
    {
        _service = new ProductService();
    }

    [HttpGet]
    [Route("")]
    public IHttpActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public IHttpActionResult Get(int id)
    {
        return Ok(_service.GetById(id));
    }

    [HttpPut]
    [Route("")]
    public IHttpActionResult Update(PRDTB1 product)
    {
        _service.Update(product);
        return Ok("Updated & cache cleared");
    }
}
