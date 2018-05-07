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
		[HttpGet][Route("api/Cv/{idCV}/PerStud/")]
		public IEnumerable<PerStud> Get()
		{
			return dm.GetPerStudi("{idCV}");
		}

		// GET api/<controller>/5
		[HttpGet][Route("api/Cv/{idCV}/PerStud/{id}")]
		public PerStud Get(int id)
		{
			return dm.GetPercorso(id);
		}

		[HttpPost][Route("api/Cv/{idCV}/PerStud/")]
		public void Post([FromBody]PerStud percorso)
		{
			dm.AddCvStudi(idCv,percorso);
		}

		// PUT api/<controller>/5
		public void Put(int id,[FromBody]string value)
		{
			dm.ModPerStudi()
		}

		// DELETE api/<controller>/5
		[HttpDelete][Route("api/Cv/{idCV}/PerStud/{id}")]
		public void Delete(int id)
		{
			dm.DelPerStud(id);
		}
	}
}