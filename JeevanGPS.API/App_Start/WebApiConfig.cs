using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace JeevanGPS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                //, new { id = new ParamNotAllowedForMethod("POST") }
            );
        }
    }


    public class ParamNotAllowedForMethod : IRouteConstraint
    {
        string method;

        public ParamNotAllowedForMethod(string method)
        {
            this.method = method;
        }

        public bool Match(
            HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.IncomingRequest &&
                httpContext.Request.HttpMethod == method &&
                values[parameterName] != null)
            {
                return false;
            }

            return true;
        }
    }


}
