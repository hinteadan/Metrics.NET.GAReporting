using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public class Meter : MetricGroup
    {
        private const string counterCategory = "Meter Count";
        private const string gaugeCategory = "Meter Gauge";

        private readonly Counter counter;

        private readonly Gauge gaugeOfRate;

        private readonly ICanReportToGoogleAnalytics[] metrics;

        public Meter(string name, string unit, string rateUnit, long count, double rate, string categoryPrefix)
        {
            this.counter = new Counter(string.Format("{0} Count", name), count, unit, Prefix(counterCategory, categoryPrefix));
            this.gaugeOfRate = new Gauge(string.Format("{0} Gauge", name), rate, rateUnit, Prefix(gaugeCategory, categoryPrefix));
            this.metrics = new ICanReportToGoogleAnalytics[]
            {
                this.counter,
                this.gaugeOfRate,
            };
        }



        public Meter(string name, string unit, string rateUnit, long count, double rate) : this(name, unit, rateUnit, count, rate, null) { }

        public override IEnumerable<ICanReportToGoogleAnalytics> Metrics
        {
            get
            {
                return this.metrics;
            }
        }
    }
}
