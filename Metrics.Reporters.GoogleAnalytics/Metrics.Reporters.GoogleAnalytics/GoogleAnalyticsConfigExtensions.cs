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
        public static MetricsReports WithGoogleAnalytics(this MetricsReports reports, string trackingId, string clientId, TimeSpan reportingInterval)
        {
            return reports.WithReport(new GoogleAnalyticsReport(trackingId, clientId), reportingInterval);
        }

        public static MetricsReports WithGoogleAnalytics(this MetricsReports reports, string trackingId, string clientId)
        {
            return reports.WithReport(new GoogleAnalyticsReport(trackingId, clientId), TimeSpan.FromHours(1));
        }
    }
}
