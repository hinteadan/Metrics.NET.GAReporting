﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol.Values
{
    public class EventActionValue : ParameterTextValue
    {
        public EventActionValue(string value) : base(value, 500)
        {
        }
    }
}