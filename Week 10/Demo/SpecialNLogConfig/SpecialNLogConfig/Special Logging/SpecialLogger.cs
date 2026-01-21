using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using NLog.Config;

namespace SpecialNLogConfig.Special_Logging
{
    

    public class SpecialLogger : ISpecialLogger
    {
        private static readonly LogFactory _factory;
        private readonly ILogger _logger;

        static SpecialLogger()
        {
            var path = HttpContext.Current.Server.MapPath("~/NLog.Special.config");
            _factory = new LogFactory
            {
                Configuration = new XmlLoggingConfiguration(path)
            };
        }

        public SpecialLogger()
        {
            _logger = _factory.GetLogger("AuditLogger");
        }

        public void Audit(string message)
        {
            _logger.Info(message);
        }
    }

}