using System;
using Stride.Core.Diagnostics;

namespace ET
{
    public class StrideGameLogger: ILog
    {
        private Stride.Core.Diagnostics.Logger _logger;

        public StrideGameLogger()
        {
            this._logger = GlobalLogger.GetLogger("Game");
        }
        public void Trace(string msg)
        {
            _logger.Debug(msg);
        }

        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        public void Warning(string msg)
        {
            _logger.Warning(msg);
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
        }

        public void Error(Exception e)
        {
            _logger.Error(e.ToString());
        }
    }
}