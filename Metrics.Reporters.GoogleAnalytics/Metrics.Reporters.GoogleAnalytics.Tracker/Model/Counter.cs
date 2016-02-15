using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public class Counter : ICanReportToGoogleAnalytics
    {
        public ParameterTextValue HitType
        {
            get
            {
                return HitTypeValue.Event;
            }
        }

        public IEnumerable<Parameter> Parameters
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
