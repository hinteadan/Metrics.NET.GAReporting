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
        private static string rootContext = null;

        public static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(MetricsData metricData)
        {
            rootContext = metricData.Context;
            return Map(metricData, null);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(MetricsData metricData, string parentContext)
        {
            return Map(metricData.Counters, FullContext(parentContext, metricData.Context))
                .Concat(Map(metricData.Gauges, FullContext(parentContext, metricData.Context)))
                .Concat(Map(metricData.Histograms, FullContext(parentContext, metricData.Context)))
                .Concat(Map(metricData.Meters, FullContext(parentContext, metricData.Context)))
                .Concat(Map(metricData.Timers, FullContext(parentContext, metricData.Context)))
                .Union(metricData.ChildMetrics.SelectMany(m => Map(m, FullContext(parentContext, metricData.Context))));
        }

        private static Google.ICanReportToGoogleAnalytics Map(CounterValueSource counter, string context)
        {
            return new Google.Counter(FullName(counter.Name, context), counter.Value.Count, counter.Unit.Name);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<CounterValueSource> counters, string context)
        {
            return counters.Select(x => Map(x, context)).ToArray();
        }

        private static Google.ICanReportToGoogleAnalytics Map(GaugeValueSource gauge, string context)
        {
            return new Google.Gauge(FullName(gauge.Name, context), gauge.Value, gauge.Unit.Name);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<GaugeValueSource> gauges, string context)
        {
            return gauges.Select(x => Map(x, context)).ToArray();
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(HistogramValueSource histogram, string context)
        {
            return new Google.Histogram(FullName(histogram.Name, context), histogram.Unit.Name, histogram.Value.Count,
                histogram.Value.LastValue, histogram.Value.Max, histogram.Value.Min, histogram.Value.Mean, histogram.Value.StdDev,
                histogram.Value.Percentile75, histogram.Value.Percentile95, histogram.Value.Percentile98, histogram.Value.Percentile99, histogram.Value.Percentile999);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<HistogramValueSource> histograms, string context)
        {
            return histograms.SelectMany(x => Map(x, context)).ToArray();
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(MeterValueSource meter, string context)
        {
            return new Google.Meter(FullName(meter.Name, context), meter.Unit.Name, string.Format("{0}/{1}", meter.Unit.Name, TimeUnitLabel(meter.RateUnit)), meter.Value.Count, meter.Value.MeanRate);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<MeterValueSource> meters, string context)
        {
            return meters.SelectMany(x => Map(x, context)).ToArray();
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(TimerValueSource timer, string context)
        {
            return new Google.Timer(FullName(timer.Name, context), new Google.Timer.MeterParameters
            {
                Count = timer.Value.Rate.Count,
                Rate = timer.Value.Rate.MeanRate,
                Unit = timer.Unit.Name,
                RateUnit = TimeUnitLabel(timer.RateUnit)
            }, new Google.Timer.HistogramParameters
            {
                Count = timer.Value.Histogram.Count,
                Unit = TimeUnitLabel(timer.DurationUnit),
                AvgValue = timer.Value.Histogram.Mean,
                LatestValue = timer.Value.Histogram.LastValue,
                MaxValue = timer.Value.Histogram.Max,
                MinValue = timer.Value.Histogram.Min,
                StdDevValue = timer.Value.Histogram.StdDev,
                Percent75Value = timer.Value.Histogram.Percentile75,
                Percent95Value = timer.Value.Histogram.Percentile95,
                Percent98Value = timer.Value.Histogram.Percentile98,
                Percent999Value = timer.Value.Histogram.Percentile999,
                Percent99Value = timer.Value.Histogram.Percentile99
            });
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<TimerValueSource> timers, string context)
        {
            return timers.SelectMany(x => Map(x, context)).ToArray();
        }

        private static string TimeUnitLabel(TimeUnit rateUnit)
        {
            switch (rateUnit)
            {
                case TimeUnit.Days: return "d";
                case TimeUnit.Hours: return "h";
                case TimeUnit.Microseconds: return "\u00b5s";
                case TimeUnit.Milliseconds: return "ms";
                case TimeUnit.Minutes: return "m";
                case TimeUnit.Nanoseconds: return "ns";
                case TimeUnit.Seconds: return "s";
            }

            return "unknown";
        }

        private static string FullName(string name, string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                return name;
            }
            return string.Format("[{0}] {1}", context, name);
        }

        private static string FullContext(string parentContext, string context)
        {
            if (string.IsNullOrWhiteSpace(parentContext))
            {
                return context != rootContext ? context : null;
            }

            if (string.IsNullOrWhiteSpace(context))
            {
                return parentContext;
            }

            return string.Format("{0}|{1}", parentContext, context);
        }
    }
}
