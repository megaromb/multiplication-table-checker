using System;
using log4net;

namespace MTC.Engine
{
    public class MtcLog4NetLogger : IMtcLogger
    {
        private readonly ILog _logger;

        public MtcLog4NetLogger(ILog logger)
        {
            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string format, params object[] args)
        {
            _logger.InfoFormat(format, args);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string format, params object[] args)
        {
            _logger.DebugFormat(format, args);
        }

        public void Error(Exception e)
        {
            _logger.Error(string.Empty, e);
        }

        public void Error(Exception e, string message)
        {
            _logger.Error(message, e);
        }

        public void Error(Exception e, string format, params object[] args)
        {
            _logger.Error(string.Format(format, args), e);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string format, params object[] args)
        {
            _logger.ErrorFormat(format, args);
        }
    }
}
