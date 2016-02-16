using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metrics.MetricData;
using Metrics.Reporters.GoogleAnalytics.Tracker;
using Metrics.Reporters.GoogleAnalytics.Mappers;

namespace Metrics.Reporters.GoogleAnalytics
{
    public class GoogleAnalyticsReport : MetricsReport
    {
        private readonly GoogleAnalyticsTracker tracker;

        public GoogleAnalyticsReport()
        {
            this.tracker = new GoogleAnalyticsTracker("UA-73715504-2", Guid.NewGuid().ToString());
        }

        public void RunReport(MetricsData metricsData, Func<HealthStatus> healthStatus, CancellationToken token)
        {
            tracker.Track(metricsData.AsGoogleAnalytics(), token).Wait(token);
        }
    }
}
