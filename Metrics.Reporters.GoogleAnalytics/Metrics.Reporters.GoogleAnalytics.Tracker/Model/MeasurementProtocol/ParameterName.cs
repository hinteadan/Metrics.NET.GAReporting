using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public static class ParameterName
    {
        public const string Version = "v";
        public const string TrackingId = "tid";
        public const string AnonymizeIP = "aip";
        public const string DataSource = "ds";
        public const string QueueTime = "qt";
        public const string CacheBuster = "z";
        public const string ClientId = "cid";
        public const string UserId = "uid";
        public const string SessionControl = "sc";
        public const string IPOverride = "uip";
        public const string UserAgentOverride = "ua";
        public const string GeographicalOverride = "geoid";
        public const string DocumentReferrer = "dr";
        public const string ScreenResolution = "sr";
        public const string ViewportSize = "vp";
        public const string DocumentEncoding = "de";
        public const string ScreenColors = "sd";
        public const string UserLanguage = "ul";
        public const string HitType = "t";
        public const string HitNonInteractive = "ni";
        public const string DocumentLocationUrl = "dl";
        public const string DocumentHostName = "dh";
        public const string DocumentPath = "dp";
        public const string DocumentTitle = "dt";
        public const string ScreenName = "cd";
        public const string LinkId = "linkid";
        public const string ApplicationName = "an";
        public const string ApplicationId = "aid";
        public const string ApplicationVersion = "av";
        public const string ApplicationInstallerId = "aiid";
        public const string EventCategory = "ec";
        public const string EventAction = "ea";
        public const string EventLabel = "el";
        public const string EventValue = "ev";
        public const string ExceptionDescription = "exd";
        public const string ExceptionIsFatal = "exf";
        public const string CustomDimensionPrefix = "cd";
        public const string CustomMetricPrefix = "cm";

        public static readonly string[] Required = new string[] { Version, TrackingId, ClientId, HitType, ApplicationName };
    }
}
