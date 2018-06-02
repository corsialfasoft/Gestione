using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers
{
	public class PerStudController : ApiController
	{
		DomainModel dm = new DomainModel();
		// GET api/<controller>
		[Route("api/CV/{idCV}/PerStud")]
		[HttpGet]
		public IEnumerable<PerStud> Get(string idCV){
			return dm.GetPerStudi(idCV);
		}

		// GET api/<controller>/5
		[Route("api/CV/{idCv}/PerStud/{idPer}")]
		[HttpGet]
		public PerStud Get(int idPer){
			return dm.GetPercorso(idPer);
		}
		[Route("api/CV/{idCV}/PerStud/Add")]
		[HttpPost]
		public void Post([FromBody]PerStud percorso,string idCv){
			dm.AddCvStudi(idCv,percorso);
		}

		// PUT api/<controller>/5
		[Route("api/CV/{idCv}/PerStud/Put/{idPer}")]
		[HttpPut]
		public void Put(string idPer,[FromBody]PerStud percorso){
			dm.ModPerStudi(int.Parse(idPer),percorso);
		}

		// DELETE api/<controller>/5 
		[HttpDelete][Route("api/CV/{idCv}/PerStud/Del/{idPer}")]
		public void Delete(int idPer){
			dm.DelPerStud(idPer); 
		}
	}
}