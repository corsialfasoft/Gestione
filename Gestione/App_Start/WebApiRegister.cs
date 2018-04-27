using System.Web.Http;

namespace Gestione{
	public static class WebApiRegister{
		public static void Register(HttpConfiguration config){
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				name:"DefaultApi",
				routeTemplate:"api/{controller}/{id}",
				defaults: new {id = RouteParameter.Optional}
			);
		}
	}
}