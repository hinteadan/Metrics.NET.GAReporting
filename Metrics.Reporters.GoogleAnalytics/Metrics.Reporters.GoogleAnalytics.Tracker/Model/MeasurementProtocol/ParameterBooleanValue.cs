using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public class ParameterBooleanValue : ParameterValue<bool>
    {
        public ParameterBooleanValue(bool value) : base(value)
        {

        }

        public override string ToString()
        {
            return this.value ? "1" : "0";
        }
    }
}
