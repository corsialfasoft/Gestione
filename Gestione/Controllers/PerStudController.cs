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
			return null;
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id,[FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}