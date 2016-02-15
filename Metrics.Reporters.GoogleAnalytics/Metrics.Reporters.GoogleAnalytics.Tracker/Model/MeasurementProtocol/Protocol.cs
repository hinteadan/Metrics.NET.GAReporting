using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;
using MoreLinq;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public abstract class Protocol
    {
        protected const string googleHttpUrl = "http://www.google-analytics.com/collect";
        protected const string googleHttpsUrl = "https://ssl.google-analytics.com/collect";
        protected const string googleHttpBatchUrl = "http://www.google-analytics.com/batch";
        protected const string googleHttpsBatchUrl = "https://ssl.google-analytics.com/batch";

        private const string userAgent = "Metrics.NET/2.0 (Windows NT 10.0; Win64; x64; rv:2.0) Metrics.NET.GoogleAnalytics.Reporter/1 GoogleAnalytics.Tracker/1.0";

        private readonly Uri baseUrl;
        protected readonly Parameter version = Parameter.Text(ParameterName.Version, ProtocolVersionValue.Version);
        protected readonly Parameter trackingId;
        protected readonly Parameter clientId;

        protected int batchSize = 1;

        protected readonly List<Dictionary<string, Parameter>> parameterSets;

        public static Protocol Http(string trackingId, string clientId)
        {
            return new HttpProtocol(trackingId, clientId);
        }

        public static Protocol HttpBatch(string trackingId, string clientId)
        {
            return new HttpBatchProtocol(trackingId, clientId);
        }

        public static Protocol Https(string trackingId, string clientId)
        {
            return new HttpsProtocol(trackingId, clientId);
        }

        public static Protocol HttpsBatch(string trackingId, string clientId)
        {
            return new HttpsBatchProtocol(trackingId, clientId);
        }

        protected Protocol(string url, string trackingId, string clientId)
        {
            this.baseUrl = new Uri(url);
            this.trackingId = Parameter.Text(ParameterName.TrackingId, new TrackingIdValue(trackingId));
            this.clientId = Parameter.Text(ParameterName.ClientId, new ParameterTextValue(clientId, ASCIIEncoding.Unicode.GetByteCount(clientId)));

            this.parameterSets = new List<Dictionary<string, Parameter>>(new Dictionary<string, Parameter>[] {
                new Dictionary<string, Parameter> {
                    { this.version.Name, version },
                    { this.trackingId.Name, this.trackingId },
                    { this.clientId.Name, this.clientId }
                }
            });
        }

        public Protocol WithParameter(Parameter parameter)
        {
            this.parameterSets.Last()[parameter.Name] = parameter;
            return this;
        }

        public Protocol WithParameters(IEnumerable<Parameter> parameters)
        {
            foreach (var p in parameters)
            {
                this.WithParameter(p);
            }
            return this;
        }

        public Protocol Track(ParameterTextValue hitType)
        {
            this.WithParameter(Parameter.Text(ParameterName.HitType, hitType));
            this.parameterSets.Add(new Dictionary<string, Parameter> {
                { this.version.Name, version },
                { this.trackingId.Name, this.trackingId },
                { this.clientId.Name, this.clientId }
            });
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

        public IEnumerable<HitGroup> HitGroups
        {
            get
            {
                return this.parameterSets
                    .Batch(batchSize)
                    .Select(batch => new HitGroup(batch));
            }
        }


        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.parameterSets.Select(hit => string.Join("&", hit.Values.Select(p => p.ToString()))));
        }
    }
}
