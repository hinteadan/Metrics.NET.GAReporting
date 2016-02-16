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

            var counter1 = Metric.Counter("Some Counter", Unit.Calls);
            var counter2 = Metric.Counter("Some Other Counter", Unit.Events);
            var counter3 = Metric.Counter("Another Counter", Unit.Items);

            counter1.Increment();
            counter2.Increment();
            counter3.Increment();

            Console.WriteLine("Done @ {0}", DateTime.Now);
            Console.ReadKey();
        }
    }
}
