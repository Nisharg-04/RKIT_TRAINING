using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Logging
{
    public interface ILogger
    {
        Guid InstanceId { get; }
        void Log(string message);
    }

}
