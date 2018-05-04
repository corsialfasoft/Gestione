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

        [Route("api/Corsi/{idCorso}/Lezioni")]
        [HttpGet]
        public IEnumerable<Lezione> ListaLezioni(int idCorso){
            return dm.ListaLezioni(dm.SearchCorsi(idCorso));
        }

        [Route("api/Corsi/{idCorso}/Lezioni/{nomeLezione}")]
        [HttpGet]
        public Lezione DettaglioLezione(int idCorso,string nomeLezione){
            return dm.ListaLezioni(dm.SearchCorsi(idCorso)).Find(L => L.Nome.Equals(nomeLezione));
        }

        // POST api/<controller>
        public void Post([FromBody]Corso corso) {
            dm.AddCorso(corso);
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Corso corso) {
        }
        // DELETE api/<controller>/5
        public void Delete(int id) {
        }
    }
}