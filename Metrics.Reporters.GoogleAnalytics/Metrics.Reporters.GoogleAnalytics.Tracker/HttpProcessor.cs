using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Logging;
using NLog;

namespace Metrics.Reporters.GoogleAnalytics.Tracker
{
    internal static class HttpProcessor
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static async Task Post(Protocol protocol, CancellationToken cancelToken)
        {
            using (log.Timing("HTTP Call for metrics batch"))
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", protocol.UserAgent);

                log.Debug("URL: {0}", protocol.Url);
                log.Debug("With headers:{0}{1}", Environment.NewLine, string.Join(Environment.NewLine, client.DefaultRequestHeaders.Select(h => string.Format("{0}: {1}", h.Key, string.Join(",", h.Value)))));

                await Task.WhenAll(protocol.HitGroups.Select(hitGroup => PostAndLogAsync(client, protocol.Url, hitGroup.ToString(), cancelToken)));
            }
        }

        private static async Task<HttpResponseMessage> PostAndLogAsync(HttpClient client, Uri uri, string content, CancellationToken cancelToken)
        {
            log.Debug("POST Content:{0}=========={0}{1}", Environment.NewLine, content);
            return await client.PostAsync(uri, new StringContent(content), cancelToken);
        }

        public static async Task Post(Protocol protocol)
        {
            await Post(protocol, CancellationToken.None);
        }
    }
}
