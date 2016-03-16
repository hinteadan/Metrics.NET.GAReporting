using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Metrics.Reporters.GoogleAnalytics.Logging
{
    public class LogTiming : IDisposable
    {
        private readonly Logger log;
        private readonly LogLevel logLevel;
        private readonly string actionName;
        private DateTime startedAt = DateTime.MinValue;
        private DateTime endedAt = DateTime.MinValue;

        public LogTiming(Logger log, string actionName, LogLevel logLevel)
        {
            if (log == null)
            {
                throw new InvalidOperationException("A logger must be provided");
            }

            this.log = log;
            this.logLevel = logLevel;
            this.actionName = actionName;

            this.Start();
        }

        public LogTiming(Logger log, string actionName)
            : this(log, actionName, LogLevel.Debug)
        { }

        private void Start()
        {
            this.startedAt = DateTime.Now;
            log.Log(this.logLevel, "Running {0} ...", this.actionName);
        }

        private void Stop()
        {
            this.endedAt = DateTime.Now;
            log.Log(this.logLevel, "Finished {0} in {1}", this.actionName, endedAt - startedAt);
        }

        public void Dispose()
        {
            this.Stop();
        }
    }
}
