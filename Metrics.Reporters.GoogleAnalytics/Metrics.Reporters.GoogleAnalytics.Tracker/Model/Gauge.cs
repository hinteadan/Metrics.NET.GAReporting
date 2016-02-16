using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public class Gauge : Metric
    {
        private readonly string name;
        private readonly double value;
        private readonly string unit;

        public Gauge(string name, double value, string unit)
        {
            this.name = name;
            this.value = value;
            this.unit = unit;
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
                    Parameter.Text(ParameterName.EventCategory, new EventCategoryValue("Gauge")),
                    Parameter.Text(ParameterName.EventAction, new EventActionValue(string.Format("x1000 {0}", unit))),
                    Parameter.Integer(ParameterName.EventValue, new ParameterIntegerValue((long)(value * 1000)))
                });
            }
        }
    }
}
