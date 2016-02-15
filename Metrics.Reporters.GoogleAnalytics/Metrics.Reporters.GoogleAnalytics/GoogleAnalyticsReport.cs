using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metrics.MetricData;
using Metrics.Reporters.GoogleAnalytics.Tracker;

namespace Metrics.Reporters.GoogleAnalytics
{
    public class GoogleAnalyticsReport : MetricsReport
    {
        public void RunReport(MetricsData metricsData, Func<HealthStatus> healthStatus, CancellationToken token)
        {
            var tracker = new GoogleAnalyticsTracker("UA-73715504-2", Guid.NewGuid().ToString());

            tracker.Track().Wait();
        }
    }
}
