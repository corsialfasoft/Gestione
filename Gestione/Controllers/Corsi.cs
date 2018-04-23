using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interfaces;
using Gestione.Models;
using Newtonsoft.Json.Serialization;

namespace Gestione.Controllers {
    public class CorsiController : ApiController {
        DomainModel dm = new DomainModel();
        public IEnumerable<Corso> Get() {
            
            return dm.ListaCorsi().ToArray();
        }
        // GET api/<controller>/5
        public Corso Get(int id) {
            return dm.SearchCorsi(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value) {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        public void Delete(int id) {
        }
    }
}