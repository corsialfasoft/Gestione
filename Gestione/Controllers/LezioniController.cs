using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers
{
    public class LezioniController : ApiController{
        DomainModel dm = new DomainModel();
        
        [HttpGet][Route("api/Corsi/{idCorso}/Lezioni")]
        public IEnumerable<Lezione> ListaLezioni(int idCorso){
            return dm.ListaLezioni(dm.SearchCorsi(idCorso));
        }
       
        [Route("api/Corsi/{idCorso}/Lezioni/{nomeLezione}")]
        [HttpGet]
        public Lezione DettaglioLezione(int idCorso,string nomeLezione){
            return dm.ListaLezioni(dm.SearchCorsi(idCorso)).Find(L => L.Nome.Equals(nomeLezione));
        }

		[Route("api/Corsi/{idCorso}/Lezioni")]
		[HttpPost]
		public void AddLezione(int IdCorso,[FromBody]Lezione lezione) {
		dm.AddLezione(IdCorso,lezione );
		}


		[Route("api/Corsi/{idCorso}/Lezioni/{Id}")]
        [HttpDelete]
		public void DeleteLezione(int Id) {
		dm.EliminaLezione(Id);
		}

		[Route("api/Corsi/{idCorso}/Lezioni/{Id}")]
		[HttpPut]
		public void ModificaLezione(Lezione lezione) {
		dm.ModLezione(lezione);
		}
    }
}
