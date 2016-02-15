using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public class Parameter
    {
        private readonly string name;
        private ParameterValue value;

        public static Parameter Text(string name, ParameterTextValue value)
        {
            return new Parameter(name, value);
        }

        public static Parameter Currency(string name, ParameterCurrencyValue value)
        {
            return new Parameter(name, value);
        }

        public static Parameter Integer(string name, ParameterIntegerValue value)
        {
            return new Parameter(name, value);
        }

        public static Parameter Boolean(string name, ParameterBooleanValue value)
        {
            return new Parameter(name, value);
        }

        private Parameter(string name, ParameterValue value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}={1}", this.name, this.value.ToStringForPayload());
        }
    }
}
