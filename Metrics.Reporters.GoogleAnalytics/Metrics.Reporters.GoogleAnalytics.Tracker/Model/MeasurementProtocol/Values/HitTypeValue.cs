using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values
{
    public static class HitTypeValue
    {
        public static ParameterTextValue PageView = new ParameterTextValue("pageview", ASCIIEncoding.Unicode.GetByteCount("pageview"));
        public static ParameterTextValue ScreenView = new ParameterTextValue("screenview", ASCIIEncoding.Unicode.GetByteCount("screenview"));
        public static ParameterTextValue Event = new ParameterTextValue("event", ASCIIEncoding.Unicode.GetByteCount("event"));
        public static ParameterTextValue Transaction = new ParameterTextValue("transaction", ASCIIEncoding.Unicode.GetByteCount("transaction"));
        public static ParameterTextValue Item = new ParameterTextValue("item", ASCIIEncoding.Unicode.GetByteCount("item"));
        public static ParameterTextValue Social = new ParameterTextValue("social", ASCIIEncoding.Unicode.GetByteCount("social"));
        public static ParameterTextValue Exception = new ParameterTextValue("exception", ASCIIEncoding.Unicode.GetByteCount("exception"));
        public static ParameterTextValue Timing = new ParameterTextValue("timing", ASCIIEncoding.Unicode.GetByteCount("timing"));
    }
}
