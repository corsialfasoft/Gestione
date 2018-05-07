using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
	public class CompetenzeController : ApiController {
		DomainModel dm = new DomainModel();
		// GET api/<controller>
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		[HttpGet][Route("api/CV/{idCV}/Competenza/{idCompetenza}")]		//FINIRE
		public Competenza GetCompetenza(int idCompetenza) {
			return dm.GetCompetenza(idCompetenza);
		}

		// POST api/<controller>
		[HttpPost][Route("api/CV/{idCV}/Competenza")]
		public void Post([FromBody]Competenza competenza, int idCV) {
			dm.AddCompetenze($"{idCV}", competenza);
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/<controller>/5
		[HttpGet][Route("api/CV/{idCV}/Competenza/{idCompetenza}")]      //FINIRE
		public void Delete([FromBody]int idCV) {
			dm.DelCompetenza(idCV);
		}
	}
}