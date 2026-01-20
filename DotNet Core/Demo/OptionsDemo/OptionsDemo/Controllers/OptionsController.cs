using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OptionsDemo.Options;

namespace OptionsDemo.Controllers
{
    [ApiController]
    [Route("api/options")]
    public class OptionsController : ControllerBase
    {
        private readonly IOptions<AppSettings> _options;
        private readonly IOptionsSnapshot<AppSettings> _snapshot;
        private readonly IOptionsMonitor<AppSettings> _monitor;
        private readonly IOptionsSnapshot<EmailSettings> _email;

        public OptionsController(
            IOptions<AppSettings> options,
            IOptionsSnapshot<AppSettings> snapshot,
            IOptionsMonitor<AppSettings> monitor,
            IOptionsSnapshot<EmailSettings> email)
        {
            _options = options;
            _snapshot = snapshot;
            _monitor = monitor;
            _email = email;
        }

        [HttpGet("compare")]
        public IActionResult Compare()
        {
            return Ok(new
            {
                IOptions = _options.Value,
                IOptionsSnapshot = _snapshot.Value,
                IOptionsMonitor = _monitor.CurrentValue,
                Gmail = _email.Get("Gmail"),
                Outlook = _email.Get("Outlook")
            });
        }
    }

}
