using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.MetricData;
using Google = Metrics.Reporters.GoogleAnalytics.Tracker.Model;

namespace Metrics.Reporters.GoogleAnalytics.Mappers
{
    internal static class MetricsToGoogleMapper
    {
        public static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(MetricsData metricData)
        {
            return Map(metricData.Counters)
                .Concat(Map(metricData.Gauges));
        }

        private static Google.ICanReportToGoogleAnalytics Map(CounterValueSource counter)
        {
            return new Google.Counter(counter.Name, counter.Value.Count, counter.Unit.Name);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<CounterValueSource> counters)
        {
            return counters.Select(Map).ToArray();
        }

        private static Google.ICanReportToGoogleAnalytics Map(GaugeValueSource gauge)
        {
            return new Google.Gauge(gauge.Name, gauge.Value, gauge.Unit.Name);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<GaugeValueSource> gauges)
        {
            return gauges.Select(Map).ToArray();
        }


    }
}
