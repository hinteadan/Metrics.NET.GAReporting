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
                .Concat(Map(metricData.Gauges))
                .Concat(Map(metricData.Histograms))
                .Concat(Map(metricData.Meters))
                .Concat(Map(metricData.Timers));
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

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(HistogramValueSource histogram)
        {
            return new Google.Histogram(histogram.Name, histogram.Unit.Name, histogram.Value.Count,
                histogram.Value.LastValue, histogram.Value.Max, histogram.Value.Min, histogram.Value.Mean, histogram.Value.StdDev,
                histogram.Value.Percentile75, histogram.Value.Percentile95, histogram.Value.Percentile98, histogram.Value.Percentile99, histogram.Value.Percentile999);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<HistogramValueSource> histograms)
        {
            return histograms.SelectMany(Map).ToArray();
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(MeterValueSource meter)
        {
            return new Google.Meter(meter.Name, meter.Unit.Name, string.Format("{0}/{1}", meter.Unit.Name, TimeUnitLabel(meter.RateUnit)), meter.Value.Count, meter.Value.MeanRate);
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<MeterValueSource> meters)
        {
            return meters.SelectMany(Map).ToArray();
        }

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(TimerValueSource timer)
        {
            return new Google.Timer(timer.Name, new Google.Timer.MeterParameters
            {
                Count = timer.Value.Rate.Count,
                Rate = timer.Value.Rate.MeanRate,
                Unit = timer.Unit.Name,
                RateUnit = TimeUnitLabel(timer.RateUnit),
                DurationUnit = TimeUnitLabel(timer.DurationUnit)
            }, new Google.Timer.HistogramParameters
            {
                Count = timer.Value.Histogram.Count,
                Unit = timer.Unit.Name,
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

        private static IEnumerable<Google.ICanReportToGoogleAnalytics> Map(IEnumerable<TimerValueSource> timers)
        {
            return timers.SelectMany(Map).ToArray();
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
    }
}
