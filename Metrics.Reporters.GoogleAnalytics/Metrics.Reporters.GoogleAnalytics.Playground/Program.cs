using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                .WithReporting(cfg => cfg.WithGoogleAnalytics("UA-73715504-2", Guid.NewGuid().ToString(), TimeSpan.FromSeconds(10)));

            var timer = Metric.Timer("Awesome Processor", Unit.Requests, SamplingType.FavourRecent, TimeUnit.Microseconds, TimeUnit.Microseconds);

            using (timer.NewContext())
            {
                Thread.Sleep(133);
            }

            Console.WriteLine("Done @ {0}", DateTime.Now);
            Console.ReadKey();
        }
    }
}
