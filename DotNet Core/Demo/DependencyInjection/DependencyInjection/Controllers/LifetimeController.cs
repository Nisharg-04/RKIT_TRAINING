using DependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("api/lifetime")]
    public class LifetimeController : ControllerBase
    {
        private readonly TransientService _t1;
        private readonly TransientService _t2;

        private readonly ScopedService _s1;
        private readonly ScopedService _s2;

        private readonly SingletonService _sg1;
        private readonly SingletonService _sg2;

        [FromServices]
        public TransientService T { get; set; }

        public LifetimeController(
            TransientService t1,
            TransientService t2,
            ScopedService s1,
            ScopedService s2,
            SingletonService sg1,
            SingletonService sg2)
        {
            _t1 = t1;
            _t2 = t2;
            _s1 = s1;
            _s2 = s2;
            _sg1 = sg1;
            _sg2 = sg2;
        }

        [HttpGet("constructor")]
        public IActionResult ConstructorDemo()
        {
            return Ok(new
            {
                Transient1 = _t1.InstanceId,
                Transient2 = _t2.InstanceId,

                Scoped1 = _s1.InstanceId,
                Scoped2 = _s2.InstanceId,

                Singleton1 = _sg1.InstanceId,
                Singleton2 = _sg2.InstanceId
            });
        }
        [HttpGet("method")]
        public IActionResult MethodInjection(
        [FromServices] TransientService t,
        [FromServices] ScopedService s,
        [FromServices] SingletonService sg) 
        {
            return Ok(new
            {
                Transient = t.InstanceId,
                Scoped = s.InstanceId,
                Singleton = sg.InstanceId
            });
        }

        [HttpGet("property")]
        public IActionResult PropertyInjection()
        {
            return Ok(new
            {
                T.InstanceId
            });
        }

    }


}
