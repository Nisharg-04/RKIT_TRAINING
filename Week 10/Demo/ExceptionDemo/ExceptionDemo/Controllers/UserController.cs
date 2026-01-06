using ExceptionDemo.Exceptions;
using System;
using System.Web.Http;

[RoutePrefix("api/users")]
public class UsersController : ApiController
{
    [HttpGet]
    [Route("{id}")]
    public IHttpActionResult GetUser(int id)
    {
        if (id <= 0)
            throw new BusinessException(
                "User id must be greater than zero",
                "INVALID_USER_ID");

        if (id == 99)
            throw new Exception("Database connection failed");

        return Ok(new
        {
            Id = id,
            Name = "Nisharg Soni"
        });
    }
}
