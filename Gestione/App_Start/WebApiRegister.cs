using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Gestione {
    public static class WebApiRegister {
        public static void Register(HttpConfiguration config) {
            // Servizi e configurazione dell'API Web

            // Route dell'API Web
			
			config.EnableCors(new EnableCorsAttribute("http://localhost:4200" , headers: "*" , methods: "*" ));
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}