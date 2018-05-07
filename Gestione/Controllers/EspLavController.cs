using Gestione.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Interfaces;

namespace Gestione.Controllers {
    public class EspLavController : ApiController {
        DomainModel dm = new DomainModel();
        
        [Route("api/CV/{idCV}/EspLav")]
        [HttpGet]
        public IEnumerable<EspLav> ListaLezioni(string matricola){
            return dm.GetEspLav(matricola);
        }
       
        [Route("api/CV/{idCV}/EspLav/{nomeLezione}")]
        [HttpGet]
        public Lezione DettaglioLezione(int idCorso,string nomeLezione){
            return dm.ListaLezioni(dm.SearchCorsi(idCorso)).Find(L => L.Nome.Equals(nomeLezione));
        }

        [Route("api/CV/{idCV}/EspLav/{nomeLezione}")]
        [HttpGet]
        public Lezione DettaglioLezione(int idCorso,string nomeLezione){
            return dm.ListaLezioni(dm.SearchCorsi(idCorso)).Find(L => L.Nome.Equals(nomeLezione));
        }

    }
}
