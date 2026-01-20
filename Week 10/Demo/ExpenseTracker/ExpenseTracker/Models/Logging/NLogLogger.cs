using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

using System.Threading.Tasks;

namespace ExpenseTracker.Models.Logging
{
    public class NLogLogger : INLogLogger
    {
        private static readonly Logger _logger = LogManager.GetLogger("ExpenseTracker");

        public NLogLogger()
        {
            _logger.Info("Logger Initialized");
        }
        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }
        public void Error(string message)
        {
            _logger.Error(message);

        }
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }
        public void Debug(string message)
        {
                
            _logger.Debug(message);
        }
        public void Trace(string message)
        {
            _logger.Trace(message);
        }

    }
}
