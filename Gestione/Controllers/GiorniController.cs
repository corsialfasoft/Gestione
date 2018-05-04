using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gestione.Models;

namespace Gestione.Controllers {
    public class GiorniController : ApiController {
        DomainModel dm = new DomainModel();
        // GET api/<controller>
        public IEnumerable<DTGiornoDMese> Get() {
            //return new string[] { "value1","value2" };
            return dm.DettaglioMese(2018, 4, "prova");
        }

        // GET api/<controller>/5
        public DTGGiorno Get(string id) {
            //return "value";
            return dm.VisualizzaGiorno(DateTime.Parse(id),"prova");
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