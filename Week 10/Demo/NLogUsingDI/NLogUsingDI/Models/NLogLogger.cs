using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLogUsingDI.Models
{
    public class NLogLogger : ILogger
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
         
        public NLogLogger()
        {
            _logger.Info("Logger Initialized");
        }
        public void Info(string message)
        {
            _logger?.Info(message);
        }

        public void Warn(string message)
        {
            _logger?.Warn(message);
        }
        public void Error(string message)
        {
            _logger?.Error(message);

        }
        public void Fatal(string message)
        {
            _logger?.Fatal(message);
        }
        public void Debug(string message)
        {

            _logger?.Debug(message);
        }
        public void Trace(string message)
        {
            _logger?.Trace(message);
        }

    }
}