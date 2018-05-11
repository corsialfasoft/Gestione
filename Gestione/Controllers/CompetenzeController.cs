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
		[Route("api/CV/{idCV}/Competenza")] // OK!
		[HttpGet]
		public IEnumerable<Competenza> Get(string idCV) {
			return dm.GetComp(idCV);
		}


		// GET api/<controller>/5
		[HttpGet][Route("api/Competenza/{idCompetenza}")]		//OK!
		public Competenza GetCompetenza(int idCompetenza) {
			return dm.GetCompetenza(idCompetenza);
		}

		// POST api/<controller>
		[HttpPost][Route("api/CV/{idCV}/Add/Competenza")]		//OK!
		public void Post([FromBody]Competenza competenza, string idCV) {
			dm.AddCompetenze(idCV, competenza);
		}

		// PUT api/<controller>/5
		[Route("api/Competenza/Put/{idCompetenza}")]	//OK!
		[HttpPut]
		public void Put(int idCompetenza, [FromBody]Competenza competenza) {
			dm.ModComp(idCompetenza, competenza);
		}

		// DELETE api/<controller>/5
		[HttpDelete][Route("api/Competenza/Del/{idCompetenza}")]      //FINIRE
		public void Delete(int idCompetenza) {
			dm.DelCompetenza(idCompetenza);
		}
	}
}