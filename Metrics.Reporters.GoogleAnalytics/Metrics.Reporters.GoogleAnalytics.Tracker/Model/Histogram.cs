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
            double percent75, double percent95, double percent98, double percent99, double percent999)
        {
            this.counter = new Counter(string.Format("{0} Count", name), count, unit, counterCategory);

            this.gaugeOfLatestValue = new Gauge(string.Format("{0} Last Value", name), latestValue, unit, gaugeCategory);
            this.gaugeOfMaxValue = new Gauge(string.Format("{0} Max Value", name), maxValue, unit, gaugeCategory);
            this.gaugeOfAvgValue = new Gauge(string.Format("{0} Avg Value", name), avgValue, unit, gaugeCategory);
            this.gaugeOfMinValue = new Gauge(string.Format("{0} Min Value", name), minValue, unit, gaugeCategory);
            this.gaugeOfStdDevValue = new Gauge(string.Format("{0} StdDev Value", name), stdDevValue, unit, gaugeCategory);

            this.gaugeOfPerecent75 = new Gauge(string.Format("{0} Percentile 75", name), percent75, unit, gaugeCategory);
            this.gaugeOfPerecent95 = new Gauge(string.Format("{0} Percentile 95", name), percent95, unit, gaugeCategory);
            this.gaugeOfPerecent98 = new Gauge(string.Format("{0} Percentile 98", name), percent98, unit, gaugeCategory);
            this.gaugeOfPerecent99 = new Gauge(string.Format("{0} Percentile 99", name), percent99, unit, gaugeCategory);
            this.gaugeOfPerecent999 = new Gauge(string.Format("{0} Percentile 999", name), percent999, unit, gaugeCategory);

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

        public override IEnumerable<ICanReportToGoogleAnalytics> Metrics
        {
            get
            {
                return this.metrics;
            }
        }
    }
}
