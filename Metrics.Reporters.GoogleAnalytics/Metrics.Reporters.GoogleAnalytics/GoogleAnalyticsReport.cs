using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metrics.MetricData;
using Metrics.Reporters.GoogleAnalytics.Tracker;
using Metrics.Reporters.GoogleAnalytics.Mappers;
using Metrics.Reporters.GoogleAnalytics.Logging;
using NLog;

namespace Metrics.Reporters.GoogleAnalytics
{
    public class GoogleAnalyticsReport : MetricsReport
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly GoogleAnalyticsTracker tracker;

        public GoogleAnalyticsReport(string trackingId, string clientId)
        {
            this.tracker = new GoogleAnalyticsTracker(trackingId, clientId);
        }

        public void RunReport(MetricsData metricsData, Func<HealthStatus> healthStatus, CancellationToken token)
        {
            using (log.Timing("Report of metrics data to Google Analytics"))
            {
                tracker.Track(metricsData.AsGoogleAnalytics(), token).Wait(token);
            }
        }
    }
}
