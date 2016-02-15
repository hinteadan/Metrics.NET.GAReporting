using Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model
{
    public interface ICanReportToGoogleAnalytics
    {
        ParameterTextValue HitType { get; }

        IEnumerable<Parameter> Parameters { get; }
    }
}
