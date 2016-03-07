﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metrics.MetricData;
using Metrics.Reporters.GoogleAnalytics.Tracker;
using Metrics.Reporters.GoogleAnalytics.Mappers;

namespace Metrics.Reporters.GoogleAnalytics
{
    public class GoogleAnalyticsReport : MetricsReport
    {
        private readonly GoogleAnalyticsTracker tracker;

        public GoogleAnalyticsReport(string trackingId, string clientId)
        {
            this.tracker = new GoogleAnalyticsTracker(trackingId, clientId);
        }

        public void RunReport(MetricsData metricsData, Func<HealthStatus> healthStatus, CancellationToken token)
        {
            var allMetrics = new MetricsData[] { metricsData }.Concat(metricsData.ChildMetrics);

            Task.WaitAll(allMetrics.Select(m => tracker.Track(metricsData.AsGoogleAnalytics(), token)).ToArray(), token);
        }
    }
}
