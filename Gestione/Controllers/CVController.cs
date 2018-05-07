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
		public void Post([FromBody]CV cv) {
            dm.AggiungiCV(cv);
        }
		public void Delete(string id ){
			dm.EliminaCV(dm.Search(id));
		}
    }
}
