using System;
using NLog;

namespace Hdd.Logger
{
    public interface ILogger
    {
        void Degug(object context, string message, Exception exception = null);
        void Info(object context, string message, Exception exception = null);
        void Warn(object context, string message, Exception exception = null);
        void Error(object context, string message, Exception exception = null);
        void Fatal(object context, string message, Exception exception = null);
    }

    public class Logger : ILogger
    {
        private readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public void Degug(object context, string message, Exception exception = null)
        {
            _logger.Log(new LogEventInfo(LogLevel.Debug, context.GetType().Name, message) {Exception = exception});
        }

        public void Info(object context, string message, Exception exception = null)
        {
            _logger.Log(new LogEventInfo(LogLevel.Info, context.GetType().Name, message) {Exception = exception});
        }

        public void Warn(object context, string message, Exception exception = null)
        {
            _logger.Log(new LogEventInfo(LogLevel.Warn, context.GetType().Name, message) {Exception = exception});
        }

        public void Error(object context, string message, Exception exception = null)
        {
            var eventInfo = new LogEventInfo(LogLevel.Error, context.GetType().Name, message) {Exception = exception};
            _logger.Log(eventInfo);
        }

        public void Fatal(object context, string message, Exception exception = null)
        {
            _logger.Log(new LogEventInfo(LogLevel.Fatal, context.GetType().Name, message) {Exception = exception});
        }
    }
}