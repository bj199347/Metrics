using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web;
using System.Web.Hosting;
using System.Reflection;
using System;
using System.Web.Http.Description;

namespace  AspNetSelfHostDemo
{
    public class SportsBackOfficeController:ApiController
    {
        [Route("{*actionName}")]
        [AcceptVerbs("GET", "POST", "PUT", "DELETE")]//Include what ever methods you want to handle
        [AllowAnonymous]//So I can use it on authenticated controllers
        [ApiExplorerSettings(IgnoreApi = true)]//To hide this method from helpers
        public virtual HttpResponseMessage HandleUnknownAction(string actionName)
        {
            var response = new HttpResponseMessage();
            
            response.Content = new StringContent("Get Started with the Sports Back Office API");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        [Route("{format}/fixture/{fixtureid}")]
        public HttpResponseMessage GetFixtureDetailsById(string format, int fixtureid)
        {
            // Call database to get the Fixture Details By Id

            // Serialize to JSON string
            string json = "{fixtureid:"+fixtureid.ToString()+"}";

            return ConstructResponse(format, json, "Fixture Details:" + fixtureid.ToString());
        }

        private HttpResponseMessage ConstructResponse(string format, string json, string htmlPageTitle)
        {
            var response = new HttpResponseMessage();

            if (format.ToLower() == "json")
            {
                response.Content = new StringContent(json);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/json");
            }
            else if (format.ToLower() == "html")
            {
                response.Content = new StringContent(GetHtmlPageText(json, htmlPageTitle));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            }
            else if (format.ToLower() == "file")
            {
                response.Content = new StringContent(json);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = String.Format("{0}.txt", htmlPageTitle)
                };
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Content = new StringContent("Invalid response type requested in the request URL.  Options are json and html.");
            }

            return response;
        }

        private string GetHtmlPageText(string json, string pageTitle)
        {
            string pageText = "";
            var path = HostingEnvironment.MapPath("~/HTML/JsonFormatter.html");
            if (path == null)
            {
                var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                path = new Uri(uriPath).LocalPath + "/HTML/JsonFormatter.html";
            }
            pageText = File.ReadAllText(path);
            return pageText.Replace("{{JSON}}", json).Replace("{{TITLE}}", pageTitle);
        }
    }
}
