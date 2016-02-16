using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.MetricData;
using Google = Metrics.Reporters.GoogleAnalytics.Tracker.Model;

namespace Metrics.Reporters.GoogleAnalytics.Mappers
{
    internal static class Extensions
    {
        public static IEnumerable<Google.ICanReportToGoogleAnalytics> AsGoogleAnalytics(this MetricsData metrics)
        {
            return MetricsToGoogleMapper.Map(metrics);
        }
    }
}
