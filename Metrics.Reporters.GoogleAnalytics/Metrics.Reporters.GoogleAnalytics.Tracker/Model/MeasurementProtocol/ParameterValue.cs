using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public abstract class ParameterValue<T>
    {
        protected readonly T value;

        public ParameterValue(T value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", this.value);
        }

        public virtual string ToStringForPayload()
        {
            return WebUtility.UrlEncode(this.ToString());
        }
    }
}
