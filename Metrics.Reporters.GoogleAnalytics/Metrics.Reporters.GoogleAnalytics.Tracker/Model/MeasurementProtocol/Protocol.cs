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

        private const string userAgent = "Metrics.NET/2.0 (Windows NT 10.0; Win64; x64; rv:2.0) Metrics.NET.GoogleAnalytics.Reporter/1 GoogleAnalytics.Tracker/1.0";

        private readonly Uri baseUrl;
        protected readonly Parameter version = Parameter.Text(ParameterName.Version, ProtocolVersionValue.Version);
        protected readonly Parameter trackingId;
        protected readonly Parameter clientId;

        protected readonly Dictionary<string, Parameter> parameters;

        protected Protocol(string url, string trackingId, string clientId)
        {
            this.baseUrl = new Uri(url);
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

        public Uri Url
        {
            get
            {
                return baseUrl;
            }
        }

        public string UserAgent
        {
            get
            {
                return userAgent;
            }
        }


        public override string ToString()
        {
            return string.Join("&", this.parameters.Values);
        }
    }
}
