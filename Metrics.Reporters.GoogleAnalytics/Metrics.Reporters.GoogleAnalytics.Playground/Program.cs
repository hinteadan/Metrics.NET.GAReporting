using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker;

namespace Metrics.Reporters.GoogleAnalytics.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Metric.Config
                .WithAllCounters()
                .WithInternalMetrics()
                .WithReporting(cfg => cfg.WithGoogleAnalytics());

            var meter = Metric.Meter("Meter Sample", Unit.Calls, TimeUnit.Microseconds);

            var histogram = Metric.Histogram("Some Histogram", Unit.Requests);

            var counters = new Counter[] {
                Metric.Counter("Some Counter", Unit.Calls),
                Metric.Counter("Some Other Counter", Unit.Events)
            };

            Metric.Gauge("Some Gauge", () => 33.656, Unit.Custom("kbps"));
            Metric.Gauge("Some Other Gauge", () => 1332.3, Unit.Custom("m/s"));


            counters[0].Increment();
            counters[1].Increment();

            histogram.Update(10);
            histogram.Update(15);
            histogram.Update(3);
            histogram.Update(50);
            histogram.Update(1);

            meter.Mark(10);
            meter.Mark(1);
            meter.Mark(4);
            meter.Mark(3);

            Console.WriteLine("Done @ {0}", DateTime.Now);
            Console.ReadKey();
        }
    }
}
