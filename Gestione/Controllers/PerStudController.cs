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
		[Route("api/Cv/{idCV}/PerStud")]
		[HttpGet]
		public IEnumerable<PerStud> Get(string idCV)
		{
			return dm.GetPerStudi(idCV);
		}

		// GET api/<controller>/5
		[Route("api/Cv/{idCV}/PerStud/{idPer}")]
		[HttpGet]
		public PerStud Get(int idPer)
		{
			return dm.GetPercorso(idPer);
		}

		[HttpPost][Route("api/Cv/{idCV}/PerStud")]
		public void Post([FromBody]PerStud percorso,string idCv)
		{
			dm.AddCvStudi(idCv,percorso);
		}

		// PUT api/<controller>/5
		[HttpPut][Route("api/Cv/{idCV}/PerStud/{idPer}")]
		public void Put(string idCV,string idPer,[FromBody]PerStud percorso)
		{
			dm.ModPerStudi(int.Parse(idPer),percorso);
		}

		// DELETE api/<controller>/5 
		[HttpDelete][Route("api/Cv/{idCV}/PerStud/{idPer}")]
		public void Delete(int idPer,string idCV)
		{
			dm.DelPerStud(idPer); 
		}
	}
}