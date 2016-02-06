using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metrics.MetricData;

namespace Metrics.Reporters.GoogleAnalytics
{
    public class GoogleAnalyticsReport : MetricsReport
    {
        public void RunReport(MetricsData metricsData, Func<HealthStatus> healthStatus, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
