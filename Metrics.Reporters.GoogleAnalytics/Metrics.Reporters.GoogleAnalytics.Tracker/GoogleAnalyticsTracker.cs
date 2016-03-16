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

        public GoogleAnalyticsTracker(string trackingId)
        {
            this.trackingId = trackingId;
        }

        public async Task Track(IEnumerable<ICanReportToGoogleAnalytics> metrics, CancellationToken cancelToken)
        {
            using (log.Timing("Post metrics to Google Analytics"))
            {
                var protocol = Protocol.HttpBatch(this.trackingId);
                foreach (var metric in metrics)
                {
                    foreach (var p in metric.Parameters)
                    {
                        protocol.WithParameter(p);
                    }
                    protocol.Track(metric.HitType);

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
