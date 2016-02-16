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
        private readonly string category;
        private readonly long value;
        private readonly string unit;

        public Counter(string name, long value, string unit, string category)
        {
            this.name = name;
            this.value = value;
            this.unit = unit;
            this.category = category;
        }

        public Counter(string name, long value, string unit) : this(name, value, unit, "Counter") { }

        public Counter(string name, long value) : this(name, value, "Count", "Counter") { }

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
                    Parameter.Text(ParameterName.EventCategory, new EventCategoryValue(category)),
                    Parameter.Text(ParameterName.EventAction, new EventActionValue(unit)),
                    Parameter.Integer(ParameterName.EventValue, new ParameterIntegerValue(value))
                });
            }
        }
    }
}
