using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gestione.Models;

namespace Gestione.Controllers {
	public class GiorniController : ApiController {
		// GET api/<controller>
		DomainModel dm = new DomainModel();
		public IEnumerable<DTGiornoDMese> Get() {		
			return dm.DettaglioMese(2000,1,"MkMatric"); 
		}

		// GET api/<controller>/5
		public DTGGiorno Get(string id) {
			return dm.VisualizzaGiorno(DateTime.Parse(id),"MkMatric");
		}

		// POST api/<controller>
		public void Post([FromBody]string value) {
		}

		// PUT api/<controller>/5
		public void Put(int id,[FromBody]string value) {
		}

		// DELETE api/<controller>/5
		public void Delete(int id) {
		}
	}
}