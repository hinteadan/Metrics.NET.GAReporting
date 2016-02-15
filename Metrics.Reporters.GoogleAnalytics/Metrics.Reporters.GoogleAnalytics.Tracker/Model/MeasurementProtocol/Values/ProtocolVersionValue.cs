using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values
{
    public class ProtocolVersionValue : ParameterTextValue
    {
        public ProtocolVersionValue() : base("1", ASCIIEncoding.Unicode.GetByteCount("1"))
        {
        }
    }
}
