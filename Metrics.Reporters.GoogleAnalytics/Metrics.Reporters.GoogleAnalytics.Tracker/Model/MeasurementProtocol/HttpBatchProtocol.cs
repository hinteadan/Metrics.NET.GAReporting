using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    internal class HttpBatchProtocol : Protocol
    {
        public HttpBatchProtocol(string trackingId, string clientId) : base(Protocol.googleHttpBatchUrl, trackingId, clientId)
        {
        }
    }
}
