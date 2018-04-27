using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Gestione.App_Start {
	public class WebApiResourceRegister {
		public static void Register(HttpConfiguration config){
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(			
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
		
			);
		}
	}
}