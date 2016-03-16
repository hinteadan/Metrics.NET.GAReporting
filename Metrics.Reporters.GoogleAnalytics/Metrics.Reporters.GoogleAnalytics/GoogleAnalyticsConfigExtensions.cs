using Metrics.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics
{
    public static class GoogleAnalyticsConfigExtensions
    {
        public static MetricsReports WithGoogleAnalytics(this MetricsReports reports, string trackingId, TimeSpan reportingInterval)
        {
            return reports.WithReport(new GoogleAnalyticsReport(trackingId), reportingInterval);
        }

        public static MetricsReports WithGoogleAnalytics(this MetricsReports reports, string trackingId)
        {
            return reports.WithReport(new GoogleAnalyticsReport(trackingId), TimeSpan.FromMinutes(10));
        }
    }
}
