using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public abstract class Metric : ICanReportToGoogleAnalytics
    {
        public abstract string Name { get; }

        public virtual ParameterTextValue HitType
        {
            get
            {
                return HitTypeValue.Event;
            }
        }

        public virtual IEnumerable<Parameter> Parameters
        {
            get
            {
                return new Parameter[] {
                    Parameter.Boolean(ParameterName.HitNonInteractive, ParameterBooleanValue.True),
                    Parameter.Text(ParameterName.EventLabel, new EventLabelValue(this.Name))
                };
            }
        }
    }
}
