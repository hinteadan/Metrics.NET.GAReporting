using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model;
using Metrics.Reporters.GoogleAnalytics.Logging;
using System.Threading;
using NLog;

namespace Metrics.Reporters.GoogleAnalytics.Tracker
{
    public class GoogleAnalyticsTracker
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private readonly string trackingId;
        private readonly string clientId;

        public GoogleAnalyticsTracker(string trackingId, string clientId)
        {
            this.trackingId = trackingId;
            this.clientId = clientId;
        }

        public async Task Track(IEnumerable<ICanReportToGoogleAnalytics> metrics, CancellationToken cancelToken)
        {
            using (log.Timing("Post metrics to Google Analytics"))
            {
                var protocol = Protocol.HttpBatch(this.trackingId, this.clientId);
                foreach (var metric in metrics)
                {
                    protocol
                        .WithParameters(metric.Parameters)
                        .Track(metric.HitType);

                }
                await HttpProcessor.Post(protocol, cancelToken);
            }
        }

        public async Task Track(IEnumerable<ICanReportToGoogleAnalytics> metrics)
        {
            await Track(metrics, CancellationToken.None);
        }
    }
}
