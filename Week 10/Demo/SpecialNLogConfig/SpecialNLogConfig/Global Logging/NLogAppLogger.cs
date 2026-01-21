using NLog;
using SpecialNLogConfig.Global_Logging;
using System;

public class NLogAppLogger : IAppLogger
{
    private readonly ILogger _logger;

    public NLogAppLogger()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }

    public void Info(string message)
    {
        _logger.Info(message);
    }

    public void Error(Exception ex, string message)
    {
        _logger.Error(ex, message);
    }
}
