using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;

namespace Metrics.Reporters.GoogleAnalytics.Tracker
{
    internal static class HttpProcessor
    {
        public static async Task Post(Protocol protocol, CancellationToken cancelToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", protocol.UserAgent);

                await Task.WhenAll(protocol.HitGroups.Select(hitGroup =>
                    client.PostAsync(protocol.Url, new StringContent(hitGroup.ToString()), cancelToken)
                ));
            }
        }

        public static async Task Post(Protocol protocol)
        {
            await Post(protocol, CancellationToken.None);
        }
    }
}
