using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public class Timer : MetricGroup
    {
        private const string categoryPrefix = "Timer";

        private readonly Meter meter;
        private readonly Histogram histogram;

        private readonly Lazy<ICanReportToGoogleAnalytics[]> metrics;

        public Timer(string name, MeterParameters meter, HistogramParameters histogram)
        {
            this.meter = new Meter(string.Format("{0} Timer", name), meter.Unit, meter.RateSpeedUnit(), meter.Count, meter.Rate, categoryPrefix);
            this.histogram = new Histogram(string.Format("{0} Timer", name), histogram.Unit, histogram.Count, histogram.LatestValue, histogram.MaxValue, histogram.MinValue, histogram.AvgValue, histogram.StdDevValue, histogram.Percent75Value, histogram.Percent95Value, histogram.Percent98Value, histogram.Percent99Value, histogram.Percent999Value, categoryPrefix);

            this.metrics = new Lazy<ICanReportToGoogleAnalytics[]>(() => this.meter.Concat(this.histogram).ToArray());
        }

        public override IEnumerable<ICanReportToGoogleAnalytics> Metrics
        {
            get
            {
                return this.metrics.Value;
            }
        }

        public sealed class MeterParameters
        {
            public string Unit { get; set; }
            public string RateUnit { get; set; }
            public long Count { get; set; }
            public double Rate { get; set; }

            public string RateSpeedUnit()
            {
                return string.Format("{0}/{1}", this.Unit, this.RateUnit);
            }
        }

        public sealed class HistogramParameters
        {
            public string Unit { get; set; }
            public long Count { get; set; }
            public double LatestValue { get; set; }
            public double MinValue { get; set; }
            public double AvgValue { get; set; }
            public double MaxValue { get; set; }
            public double StdDevValue { get; set; }
            public double Percent75Value { get; set; }
            public double Percent95Value { get; set; }
            public double Percent98Value { get; set; }
            public double Percent99Value { get; set; }
            public double Percent999Value { get; set; }
        }
    }
}
