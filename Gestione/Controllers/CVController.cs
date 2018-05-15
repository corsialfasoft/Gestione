using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers{
    public class CVController : ApiController{
		DomainModel dm = new DomainModel();

		public List<CV> Get(){
			 return dm.SearchChiava("");
		}
		public CV Get(String id){
			return dm.Search(id);
		}
		[Route("api/CV/AddCV")]
		[HttpPost]
		public void Post([FromBody]CV cv) {
			//cv.Matricola = Guid.NewGuid().ToString().Substring(0,4);

            dm.AggiungiCV(cv);
        }
		public void Delete(string id ){
			dm.EliminaCV(dm.Search(id));
		}
		public void Put([FromBody] CV c){
			dm.ModificaCV(c);
		}

		[Route("api/CV/ModCV")]
		[HttpPost]
		public void ModificaCV([FromBody]CV c){
			dm.ModificaCV(c);
		}
		[Route("api/CercaCognome/{Cognome}")]
		[HttpGet]
		public List<CV> SearchCognome(string Cognome){
			return dm.SearchCognome(Cognome);
		}
		[Route("api/CercaChiava/{Chiava}")]
		[HttpGet]
		public List<CV> SearchChiava(string Chiava){
			return dm.SearchChiava(Chiava);
		}
		[Route("api/CercaEta/{Eta}")]
		[HttpGet]
		public List<CV> SearchEta(int Eta){
			return dm.SearchEta(Eta);
		}
		[Route("api/CercaMinMax/{eta_min}/{eta_max}")]
		[HttpGet]
		public List<CV> SearchMinMax(int eta_min,int eta_max){
			return dm.SearchRange(eta_min,eta_max);
		}
		
    }
}
