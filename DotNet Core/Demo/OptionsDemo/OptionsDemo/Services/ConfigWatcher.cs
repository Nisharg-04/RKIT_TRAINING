using Microsoft.Extensions.Options;
using OptionsDemo.Options;

namespace OptionsDemo.Services
{
    public class ConfigWatcher
    {
        public ConfigWatcher(IOptionsMonitor<AppSettings> monitor)
        {
            monitor.OnChange(settings =>
            {
                Console.WriteLine("Config changed:");
                Console.WriteLine(settings.MaxItems);
            });
        }
    }

}
