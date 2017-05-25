using System;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Worker.Commands;
using Worker.Converters;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            TelemetryConfiguration.Active.InstrumentationKey = "7dc90629-4819-4003-8a72-ead6376811fd";
            var msgHandler = new MessageHandler();
        }
    }
}
