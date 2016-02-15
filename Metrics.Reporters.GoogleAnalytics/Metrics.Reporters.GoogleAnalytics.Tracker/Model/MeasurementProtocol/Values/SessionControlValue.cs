using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values
{
    public static class SessionControlValue
    {
        public static ParameterTextValue Start = new ParameterTextValue("start", ASCIIEncoding.Unicode.GetByteCount("start"));
        public static ParameterTextValue End = new ParameterTextValue("end", ASCIIEncoding.Unicode.GetByteCount("start"));
    }
}
