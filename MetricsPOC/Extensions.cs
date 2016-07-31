using Metrics;
using Metrics.Json;
using Metrics.Reports;
using Metrics.Utils;
using Metrics.Visualization;
using System.Linq;

namespace MetricsPOC
{
    public static class Extensions
    {

        public static MetricsEndpointReports WithBasicCounters(this MetricsEndpointReports r)
        {           
            return r.WithEndpointReport("/metrics", (data, health, httpcontext) => 
            new MetricsEndpointResponse(JsonBuilderV2.BuildJson(data), "application/json"));
        }        

        public static MetricsEndpointReports WithMyHealthChecks(this MetricsEndpointReports r)
        {
            return r.WithEndpointReport("/marts", (data, health, httpcontext) => new MetricsEndpointResponse("marts heath checks", "text/plain"));
        }

        public static MetricsEndpointReports WithMyHealthChecks(this MetricsEndpointReports r, string endpoint)
        {
            
            return r.WithEndpointReport("/" + endpoint, (d, h, c) => new MetricsEndpointResponse("marts heath checks", "text/plain"));
        }

        public static MetricsEndpointReports WithCustomResponse(this MetricsEndpointReports r, string endpoint, string responseText)
        {
            return r.WithEndpointReport("/" + endpoint, (d, h, c) => new MetricsEndpointResponse(responseText, "text/plain"));
        }

        ///// <summary>
        ///// Register all pre-defined system performance counters as Gauge metrics.
        ///// This includes: Available RAM, CPU Usage, Disk Writes/sec, Disk Reads/sec
        ///// </summary>
        //public static MetricsConfig WithSystemCounters(this MetricsConfig config, string context)
        //{
        //    config.WithConfigExtension((ctx, hs) => PerformanceCounters.RegisterSystemCounters(ctx.Context(context)));
        //    return config;
            
        //}
}
}
