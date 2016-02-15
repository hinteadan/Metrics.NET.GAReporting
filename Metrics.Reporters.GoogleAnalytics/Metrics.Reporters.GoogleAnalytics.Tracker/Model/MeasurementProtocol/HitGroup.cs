using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public sealed class HitGroup
    {
        private readonly IEnumerable<Dictionary<string, Parameter>> group;

        public HitGroup(IEnumerable<Dictionary<string, Parameter>> group)
        {
            this.group = group;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, group.Select(hit => string.Join("&", hit.Values.Select(p => p.ToString()))));
        }
    }
}
