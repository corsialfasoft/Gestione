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
		public string Get(int id) {
			return "value";
		}

		// POST api/<controller>
		[HttpPost][Route("api/CV/{idCV}/Competenza")]
		public void Post([FromBody]string titolo, int livello) {
			Competenza c = new Competenza();
			c.Titolo = titolo;
			c.Livello = livello;
			dm.AddCompetenze(titolo, c);
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/<controller>/5
		public void Delete(int id) {
		}
	}
}