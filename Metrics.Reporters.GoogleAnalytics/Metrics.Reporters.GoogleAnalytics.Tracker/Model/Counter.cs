using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public class Counter : Metric
    {
        private readonly string name;
        private readonly long value;

        public Counter(string name, long value)
        {
            this.name = name;
            this.value = value;
        }

        public override string Name
        {
            get
            {
                return this.name;
            }
        }

        public override IEnumerable<Parameter> Parameters
        {
            get
            {
                return base.Parameters.Concat(new Parameter[] {
                    Parameter.Text(ParameterName.EventCategory, new EventCategoryValue("Counter")),
                    Parameter.Text(ParameterName.EventAction, new EventActionValue("Count")),
                    Parameter.Integer(ParameterName.EventValue, new ParameterIntegerValue(value))
                });
            }
        }
    }
}
