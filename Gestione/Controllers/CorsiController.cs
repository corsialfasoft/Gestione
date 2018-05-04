using System.Collections.Generic;
using System.Web.Http;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers {
		///<summary>
		///controller!!
		///</summary>
    public class CorsiController : ApiController {
        DomainModel dm = new DomainModel();
        ///<summary>
		///lista corsi!!
		///</summary>
		///<remarks>
		///Ottengo la lista di corsi - nota...
		///</remarks>
		public IEnumerable<Corso> Get() {
            return dm.ListaCorsi().ToArray();
        }
		///<summary>
		///lista corsi per id!!!
		///</summary>
        public Corso Get(int id) {
            Corso result= dm.SearchCorsi(id);
            //result.Lezioni = dm.ListaLezioni(result);
            return result;
        }
		///<summary>
		///aggiungi corso!!
		///</summary>
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