using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public class Histogram : MetricGroup
    {
        private const string gaugeCategory = "Histogram Gauge";
        private const string counterCategory = "Histogram Counter";

        private readonly Counter counter;

        private readonly Gauge gaugeOfLatestValue;
        private readonly Gauge gaugeOfMaxValue;
        private readonly Gauge gaugeOfAvgValue;
        private readonly Gauge gaugeOfMinValue;
        private readonly Gauge gaugeOfStdDevValue;

        private readonly Gauge gaugeOfPerecent75;
        private readonly Gauge gaugeOfPerecent95;
        private readonly Gauge gaugeOfPerecent98;
        private readonly Gauge gaugeOfPerecent99;
        private readonly Gauge gaugeOfPerecent999;

        private readonly ICanReportToGoogleAnalytics[] metrics;

        public Histogram(string name, string unit, long count, double latestValue, double maxValue, double minValue, double avgValue, double stdDevValue,
            double percent75, double percent95, double percent98, double percent99, double percent999, string categoryPrefix)
        {
            this.counter = new Counter(string.Format("{0} Count", name), count, unit, Prefix(counterCategory, categoryPrefix));

            this.gaugeOfLatestValue = new Gauge(string.Format("{0} Last Value", name), latestValue, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfMaxValue = new Gauge(string.Format("{0} Max Value", name), maxValue, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfAvgValue = new Gauge(string.Format("{0} Avg Value", name), avgValue, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfMinValue = new Gauge(string.Format("{0} Min Value", name), minValue, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfStdDevValue = new Gauge(string.Format("{0} StdDev Value", name), stdDevValue, unit, Prefix(gaugeCategory, categoryPrefix));

            this.gaugeOfPerecent75 = new Gauge(string.Format("{0} Percentile 75", name), percent75, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfPerecent95 = new Gauge(string.Format("{0} Percentile 95", name), percent95, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfPerecent98 = new Gauge(string.Format("{0} Percentile 98", name), percent98, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfPerecent99 = new Gauge(string.Format("{0} Percentile 99", name), percent99, unit, Prefix(gaugeCategory, categoryPrefix));
            this.gaugeOfPerecent999 = new Gauge(string.Format("{0} Percentile 999", name), percent999, unit, Prefix(gaugeCategory, categoryPrefix));

            this.metrics = new ICanReportToGoogleAnalytics[]
            {
                this.counter,

                this.gaugeOfLatestValue,
                this.gaugeOfMaxValue,
                this.gaugeOfAvgValue,
                this.gaugeOfMinValue,
                this.gaugeOfStdDevValue,

                this.gaugeOfPerecent75,
                this.gaugeOfPerecent95,
                this.gaugeOfPerecent98,
                this.gaugeOfPerecent99,
                this.gaugeOfPerecent999,
            };
        }

        public Histogram(string name, string unit, long count, double latestValue, double maxValue, double minValue, double avgValue, double stdDevValue,
            double percent75, double percent95, double percent98, double percent99, double percent999)
            : this(name, unit, count, latestValue, maxValue, minValue, avgValue, stdDevValue, percent75, percent95, percent98, percent99, percent999, null)
        { }

        public override IEnumerable<ICanReportToGoogleAnalytics> Metrics
        {
            get
            {
                return this.metrics;
            }
        }
    }
}
