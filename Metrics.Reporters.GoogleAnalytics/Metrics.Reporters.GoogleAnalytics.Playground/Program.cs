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

            Console.WriteLine("Done @ {0}", DateTime.Now);
            Console.ReadKey();
        }
    }
}
