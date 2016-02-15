using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model;

namespace Metrics.Reporters.GoogleAnalytics.Tracker
{
    public class GoogleAnalyticsTracker
    {
        private readonly string trackingId;
        private readonly string clientId;

        public GoogleAnalyticsTracker(string trackingId, string clientId)
        {
            this.trackingId = trackingId;
            this.clientId = clientId;
        }

        public async Task Track(IEnumerable<ICanReportToGoogleAnalytics> metrics)
        {
            await Task.WhenAll(metrics.Select(metric =>
                HttpProcessor.Post(
                    Protocol
                        .HttpBatch(this.trackingId, this.clientId)
                        .Track(metric.HitType)
                        .WithParameters(metric.Parameters)
                    )
                )
            );
        }
    }
}
