using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    internal class HttpsBatchProtocol : Protocol
    {
        public HttpsBatchProtocol(string trackingId, string clientId) : base(Protocol.googleHttpsBatchUrl, trackingId, clientId)
        {
        }
    }
}
