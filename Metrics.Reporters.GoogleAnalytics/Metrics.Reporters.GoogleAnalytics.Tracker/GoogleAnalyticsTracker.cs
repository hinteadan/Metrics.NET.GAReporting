using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;

namespace Metrics.Reporters.GoogleAnalytics.Tracker
{
    public class GoogleAnalyticsTracker
    {
        private readonly Protocol protocol;

        public GoogleAnalyticsTracker(string trackingId, string clientId)
        {
            this.protocol = Protocol.Http(trackingId, clientId);
        }

        public async Task Track()
        {
            protocol
                .Track(HitTypeValue.PageView)
                .WithParameter(Parameter.Text(ParameterName.DocumentLocationUrl, new DocumentLocationValue("http://hintee.com/home")))
                .WithParameter(Parameter.Text(ParameterName.DocumentHostName, new DocumentLocationValue("hintee.com")))
                .WithParameter(Parameter.Text(ParameterName.DocumentPath, new DocumentLocationValue("/home")))
                .WithParameter(Parameter.Text(ParameterName.DocumentTitle, new DocumentLocationValue("Home Page")));

            await HttpProcessor.Post(protocol);
        }
    }
}
