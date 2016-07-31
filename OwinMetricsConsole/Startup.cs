using System.Web.Http;
using Owin;
using Metrics;
using Owin.Metrics;
using System.Linq;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        var httpconfig = new HttpConfiguration();
        httpconfig.MapHttpAttributeRoutes();

        httpconfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "data/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        httpconfig.Routes.MapHttpRoute(
                name: "AppControlApi",
                routeTemplate: "control/{command}",
                defaults: new {controller = "control" }
            );

        httpconfig.Routes.MapHttpRoute(
                name: "VersionApi",
                routeTemplate: "version",
                defaults: new { controller = "version" }
            );

        var appXmlType = httpconfig.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
        httpconfig.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        
        Metric.Config
            .WithAllCounters()
            //.WithReporting(r => r.WithConsoleReport(TimeSpan.FromSeconds(30)))
            .WithOwin(middleware => app.Use(middleware), config => config
                 .WithMetricsEndpoint(endpointConfig =>
                 {
                     endpointConfig
                         .MetricsHealthEndpoint(endpoint: "metrics/health")
                         .MetricsPingEndpoint(endpoint: "metrics/ping")
                         .MetricsJsonEndpoint(endpoint: "metrics/json")
                         .MetricsTextEndpoint(endpoint: "metrics/text")
                         .MetricsEndpoint(endpoint: "metrics/visual");
                         //.MetricsTextEndpoint(enabled: false)
                         //.MetricsEndpoint(enabled: false);
                 })
            );

        app.UseWebApi(httpconfig);
    }
}
