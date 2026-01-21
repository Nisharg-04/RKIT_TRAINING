using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialNLogConfig.Global_Logging
{
    public interface IAppLogger
    {
        void Info(string message);
        void Error(Exception ex, string message);
    }
}
