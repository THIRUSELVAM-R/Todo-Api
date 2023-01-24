using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Cors;

namespace ToDo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            System.Web.Http.Cors.EnableCorsAttribute cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
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
