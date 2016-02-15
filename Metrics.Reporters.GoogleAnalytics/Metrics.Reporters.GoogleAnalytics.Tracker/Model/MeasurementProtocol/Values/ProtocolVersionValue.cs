using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values
{
    public static class ProtocolVersionValue
    {
        public static ParameterTextValue Version = new ParameterTextValue("1", ASCIIEncoding.Unicode.GetByteCount("1"));
    }
}
