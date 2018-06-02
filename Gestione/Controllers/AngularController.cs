using Gestione.Models;
using Interfaces;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Runtime.InteropServices;
using System.Web;
using System.Net.Http.Headers;
using System.Web.Http.WebHost;
using System.Web.Http.Cors;

namespace Gestione.Controllers{
    public class AngularController : ApiController  {
		DomainModel dm = new DomainModel();
		//HttpClient c = new HttpClient() {BaseAddress=new Uri( "http://localhost:4200")};
		//private readonly HttpWebRequest _next;
		
		public AngularController() {
			
			
		//HttpContext httpContext = new HttpContext();
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Origin","*");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Credentials","true");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Headers","Content-Type , Accept");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Methods"," POST , GET,PUT , PATCH , DELETE , OPTIONS ");
			// HttpClient http = new HttpClient();
			//	s = new Startup();
			//WebClient web = new WebClient();
			//web.Headers.Add("Access-Control-Allow-Origin","*");
			//web.Headers.Add("Access-Control-Allow-Credentials","true");
			//web.Headers.Add("Access-Control-Allow-Headers","Content-Type , Accept");
			//web.Headers.Add("Access-Control-Allow-Methods"," POST , GET,PUT , PATCH , DELETE , OPTIONS ");
			// http.BaseAddress = new Uri("http://localhost:4200/");
			
		}

		//public async Task Invoke(HttpContext httpContext) {
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Origin","*");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Credentials","true");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Headers","Content-Type , Accept");
		//	httpContext.Response.Headers.Add("Access-Control-Allow-Methods"," POST , GET,PUT , PATCH , DELETE , OPTIONS ");
			
			
		//}


	[EnableCors(origins:"http://localhost:4200" , headers:"Access-Control-Allow-Origin" , methods:"*")]

		[HttpGet]
		
		public CV[] Get(){
			
			//this.Request.Headers.Add("Access-Control-Allow-Origin" , "*");
			//this.Add("Access-Control-Allow-Origin" , "*");
				
			 return   dm.SearchChiava("").ToArray();
		}
		//public CV Get(String id){
		//	return dm.Search(id);
		//}

		[HttpGet]
		public  CV Get( string id){
			return  dm.Search(id);
		}


		//[Route("api/CV/AddCV")]
		//[HttpPost]
		//public void Post([FromBody]CV cv) {
		//	//cv.Matricola = Guid.NewGuid().ToString().Substring(0,4);

  //          dm.AggiungiCV(cv);
  //      }
		public void Delete(string id ){
			dm.EliminaCV(dm.Search(id));
		}
		public void Put([FromBody] CV c){
			dm.ModificaCV(c);
		}

		//[Route("api/CV/ModCV")]
		//[HttpPost]
		//public void ModificaCV([FromBody]CV c){
		//	dm.ModificaCV(c);
		//}
		//[Route("api/CercaCognome/{Cognome}")]
		//[HttpGet]
		//public List<CV> SearchCognome(string Cognome){
		//	return dm.SearchCognome(Cognome);
		//}
		//[Route("api/CercaChiava/{Chiava}")]
		//[HttpGet]
		//public List<CV> SearchChiava(string Chiava){
		//	return dm.SearchChiava(Chiava);
		//}
		//[Route("api/CercaEta/{Eta}")]
		//[HttpGet]
		//public List<CV> SearchEta(int Eta){
		//	return dm.SearchEta(Eta);
		//}
		//[Route("api/CercaMinMax/{eta_min}/{eta_max}")]
		//[HttpGet]
		//public List<CV> SearchMinMax(int eta_min,int eta_max){
		//	return dm.SearchRange(eta_min,eta_max);
		//}
		
    }
}
