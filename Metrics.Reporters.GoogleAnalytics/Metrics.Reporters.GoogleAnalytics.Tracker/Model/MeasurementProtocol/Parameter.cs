using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public class Parameter<T>
    {
        private readonly string name;
        private readonly ParameterValue<T> value;

        public Parameter(string name, ParameterValue<T> value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}={1}", this.name, this.value.ToStringForPayload());
        }
    }
}
