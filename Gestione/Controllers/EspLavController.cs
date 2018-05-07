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
        public IEnumerable<EspLav> Get(string idCv){
            return dm.GetEspLav(idCv);
        }
       
        [Route("api/CV/{idCV}/EspLav/{idEspLav}")]
        [HttpGet]
        public EspLav Get(int idEspLav){
            return dm.GetEsperienza(idEspLav);
        }

        [Route("api/CV/{idCV}/EspLav")]
        [HttpPost]
        public void Post(string idCv,[FromBody]EspLav el){
            dm.AddEspLav(idCv,el);
        }

        [Route("api/CV/{idCV}/EspLav/{idEspLav}")]
        [HttpPut]
        public void Put(int idEspLav, [FromBody] EspLav el){
            dm.ModEspLav(idEspLav,el);
        }
        
        [Route("api/CV/{idCV}/EspLav/{idEspLav}")]
        [HttpDelete]
        public void Delete(int idEspLav){
            dm.DelEspLav(idEspLav);
        }

    }
}
