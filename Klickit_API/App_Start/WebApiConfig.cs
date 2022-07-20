using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Klickit_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Remove xml Formatter 
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Solve Circular Exception Reference Loop
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
