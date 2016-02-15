using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values
{
    public class TrackingIdValue : ParameterTextValue
    {
        private readonly Regex trackingIdRegex = new Regex("UA-(.+)-(.+)");

        public TrackingIdValue(string value) : base(value, ASCIIEncoding.Unicode.GetByteCount(value))
        {
        }

        protected override void ValidateValue()
        {
            if (!trackingIdRegex.IsMatch(this.value))
            {
                throw new InvalidOperationException("The Google Analytics Tracking ID has an invalid format. Must be: UA-XXXX-Y");
            }

            base.ValidateValue();
        }
    }
}
