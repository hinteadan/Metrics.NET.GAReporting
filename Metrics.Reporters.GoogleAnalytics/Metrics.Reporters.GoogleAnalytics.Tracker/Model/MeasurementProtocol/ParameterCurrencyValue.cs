using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public class ParameterCurrencyValue : ParameterValue<decimal>
    {
        public ParameterCurrencyValue(decimal value) : base(value)
        {
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.0#####}", this.value);
        }
    }
}
