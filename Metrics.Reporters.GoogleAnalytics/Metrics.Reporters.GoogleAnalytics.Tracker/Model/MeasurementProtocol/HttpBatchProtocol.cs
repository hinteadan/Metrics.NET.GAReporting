using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    internal class HttpBatchProtocol : Protocol
    {
        public HttpBatchProtocol(string trackingId) : base(Protocol.googleHttpBatchUrl, trackingId)
        {
            this.batchSize = 20;
        }
    }
}
