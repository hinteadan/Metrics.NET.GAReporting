using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    internal class HttpProtocol : Protocol
    {
        public HttpProtocol(string trackingId) : base(Protocol.googleHttpUrl, trackingId)
        {
        }
    }
}
