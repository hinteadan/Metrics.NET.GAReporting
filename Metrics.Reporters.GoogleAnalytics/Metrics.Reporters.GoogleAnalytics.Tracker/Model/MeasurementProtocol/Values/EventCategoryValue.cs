﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values
{
    public class EventCategoryValue : ParameterTextValue
    {
        public EventCategoryValue(string value) : base(value, 150)
        {
        }
    }
}
