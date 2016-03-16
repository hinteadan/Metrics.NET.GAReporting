using System;
using System.Collections;
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

        protected int batchSize = 1;

        protected readonly IEnumerable<Parameter> trackingParameters;
        protected readonly List<Dictionary<string, Parameter>> parameterSets;

        public static Protocol Http(string trackingId)
        {
            return new HttpProtocol(trackingId);
        }

        public static Protocol HttpBatch(string trackingId)
        {
            return new HttpBatchProtocol(trackingId);
        }

        public static Protocol Https(string trackingId)
        {
            return new HttpsProtocol(trackingId);
        }

        public static Protocol HttpsBatch(string trackingId)
        {
            return new HttpsBatchProtocol(trackingId);
        }

        protected Protocol(string url, string trackingId)
        {
            this.baseUrl = new Uri(url);
            this.trackingId = Parameter.Text(ParameterName.TrackingId, new TrackingIdValue(trackingId));

            this.trackingParameters = new Parameter[] {
                this.version,
                this.trackingId,
                Parameter.Boolean(ParameterName.HitNonInteractive, ParameterBooleanValue.True),
                Parameter.Text(ParameterName.UserAgentOverride, new ParameterTextValue(ParameterName.UserAgentOverride, ASCIIEncoding.Unicode.GetByteCount(userAgent)))
            };

            this.parameterSets = new List<Dictionary<string, Parameter>>(
                new Dictionary<string, Parameter>[] {
                    this.trackingParameters.ToDictionary(p => p.Name, p => p)
                });
        }

        public Protocol WithParameter(Parameter parameter)
        {
            this.parameterSets.Last()[parameter.Name] = parameter;
            return this;
        }

        public Protocol WithParameters(params Parameter[] parameters)
        {
            foreach (var p in parameters)
            {
                this.WithParameter(p);
            }
            return this;
        }

        public Protocol Prepend(IEnumerable<IEnumerable<Parameter>> hits)
        {
            parameterSets.InsertRange(0, hits.Select(hit =>
                hit.Concat(this.trackingParameters).ToDictionary(p => p.Name, p => p))
            );
            return this;
        }

        public Protocol Append(IEnumerable<IEnumerable<Parameter>> hits)
        {
            parameterSets.AddRange(hits.Select(hit =>
                hit.Concat(this.trackingParameters).ToDictionary(p => p.Name, p => p))
            );
            return this;
        }

        public Protocol Track(ParameterTextValue hitType)
        {
            var userId = Guid.NewGuid().ToString();
            this.WithParameters(
                Parameter.Text(ParameterName.ClientId, new ParameterTextValue(userId, ASCIIEncoding.Unicode.GetByteCount(userId))),
                Parameter.Text(ParameterName.UserId, new ParameterTextValue(userId, ASCIIEncoding.Unicode.GetByteCount(userId))),
                Parameter.Text(ParameterName.HitType, hitType)
                );
            this.parameterSets.Add(this.trackingParameters.ToDictionary(p => p.Name, p => p));
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
                return
                    this.CleanParameterSets()
                    .Batch(batchSize)
                    .Select(batch => new HitGroup(batch));
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.parameterSets.Select(hit => string.Join("&", hit.Values.Select(p => p.ToString()))));
        }

        private IEnumerable<Dictionary<string, Parameter>> CleanParameterSets()
        {
            return this.parameterSets.Where(s => s.Keys.Contains(ParameterName.HitType));
        }
    }
}
