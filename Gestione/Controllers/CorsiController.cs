using System.Collections.Generic;
using System.Web.Http;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers {
    public class CorsiController : ApiController {
        DomainModel dm = new DomainModel();
        public IEnumerable<Corso> Get() {
            return dm.ListaCorsi().ToArray();
        }
        public Corso Get(int id) {
            Corso result= dm.SearchCorsi(id);
            //result.Lezioni = dm.ListaLezioni(result);
            return result;
        }
        // POST api/<controller>
        public void Post([FromBody]Corso corso) {
            dm.AddCorso(corso);
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Corso corso) {
			dm.ModificaCorso(id,corso);
        }
        // DELETE api/<controller>/5
        public void Delete(int id) {
			dm.EliminaCorso(id);
        }
    }
}