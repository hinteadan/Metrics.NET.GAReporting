using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public class ParameterTextValue : ParameterValue<string>
    {
        private readonly int length;

        public ParameterTextValue(string value, int length) : base(value)
        {
            this.length = length;
        }

        public override string ToStringForPayload()
        {
            ValidateValue();
            return base.ToStringForPayload();
        }

        protected virtual void ValidateValue()
        {
            if (ASCIIEncoding.Unicode.GetByteCount(this.value) > this.length)
            {
                throw new InvalidOperationException("The maximum length for the value has been exceeded. Google Analytics will truncate it thus resulting in possible invalid measurements.");
            }
        }
    }
}
