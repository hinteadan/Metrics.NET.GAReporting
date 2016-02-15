﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.Reporters.GoogleAnalytics.Tracker.Model.MeasurementProtocol
{
    public class HttpsProtocol : Protocol
    {
        protected HttpsProtocol(string trackingId, string clientId) : base(Protocol.googleHttpsUrl, trackingId, clientId)
        {
        }
    }
}