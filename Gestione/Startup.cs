using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Configuration;
using System.Web.Services.Description;
using Microsoft.Web.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace Gestione {
	public class Startup : HttpHeaders {
		
		public IConfigurationSectionHandler Configuration{ get;}
		public Startup(){
				 
			
			this.Add("Access-Control-Allow-Origin","*");
			this.Add("Access-Control-Allow-Credentials", "true");
			this.Add("Access-Control-Allow-Headers", "Content-Type , Accept");
			this.Add("Access-Control-Allow-Methods", " POST , GET,PUT , PATCH , DELETE , OPTIONS ");
		}
		//public void ConfigureServices (ServiceCollection service){
		//	HttpClient http = new HttpClient();
		//	Uri uri = new Uri("http://localhost:4200");
		//	http.BaseAddress= uri;
		//	http.
		//}
		//private Request
		//public Task Invoke( HttpContext httpContext){
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type , Accept");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Methods", " POST , GET,PUT , PATCH , DELETE , OPTIONS ");
		//	//IHttpActionResult
		//	Task task = new Task(;
		//}
	}
}