using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public abstract class Protocol
    {
        protected const string googleHttpUrl = "http://www.google-analytics.com/collect";
        protected const string googleHttpsUrl = "https://ssl.google-analytics.com/collect";

        private readonly string baseUrl;
        protected readonly Parameter version = Parameter.Text(ParameterName.Version, ProtocolVersionValue.Version);
        protected readonly Parameter trackingId;
        protected readonly Parameter clientId;

        protected readonly Dictionary<string, Parameter> parameters;

        protected Protocol(string url, string trackingId, string clientId)
        {
            this.baseUrl = url;
            this.trackingId = Parameter.Text(ParameterName.TrackingId, new TrackingIdValue(trackingId));
            this.clientId = Parameter.Text(ParameterName.ClientId, new ParameterTextValue(clientId, ASCIIEncoding.Unicode.GetByteCount(clientId)));

            this.parameters = new Dictionary<string, Parameter> {

                { this.version.Name, version },
                { this.trackingId.Name, this.trackingId },
                { this.clientId.Name, this.clientId }
            };
        }

        public Protocol WithParameter(Parameter parameter)
        {
            this.parameters[parameter.Name] = parameter;
            return this;
        }

        public Protocol Track(ParameterTextValue hitType)
        {
            this.WithParameter(Parameter.Text(ParameterName.HitType, hitType));
            return this;
        }
    }
}
