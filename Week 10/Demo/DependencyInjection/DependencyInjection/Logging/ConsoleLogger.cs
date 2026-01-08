using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyInjection.Logging
{
    public class ConsoleLogger : ILogger
    {
        public Guid InstanceId { get; } = Guid.NewGuid();

        public void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Logger [{InstanceId}] : {message}");
        }
    }

}