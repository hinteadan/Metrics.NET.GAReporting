using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public abstract class MetricGroup : IEnumerable<ICanReportToGoogleAnalytics>
    {
        public abstract IEnumerable<ICanReportToGoogleAnalytics> Metrics { get; }

        public IEnumerator<ICanReportToGoogleAnalytics> GetEnumerator()
        {
            return this.Metrics.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
