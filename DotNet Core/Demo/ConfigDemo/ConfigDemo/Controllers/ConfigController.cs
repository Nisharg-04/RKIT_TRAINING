using ConfigDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfigDemo.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly EmailService _emailService;
        private readonly PaymentService _paymentService;

        public ConfigController(
            IConfiguration config,
            EmailService emailService,
            PaymentService paymentService)
        {
            _config = config;
            _emailService = emailService;
            _paymentService = paymentService;
        }

        [HttpGet("appinfo")]
        public IActionResult GetAppInfo()
        {
            return Ok(new
            {
                Name = _config["AppInfo:Name"],
                Version = _config["AppInfo:Version"]
            });
        }

        [HttpGet("email")]
        public IActionResult SendEmail()
        {
            return Ok(_emailService.SendEmail());
        }

        [HttpGet("payment")]
        public IActionResult Pay()
        {
            return Ok(_paymentService.ProcessPayment());
        }
    }

}
