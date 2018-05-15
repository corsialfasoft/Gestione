﻿using Gestione.Models;
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
       
        [Route("api/EspLav/{idEspLav}")]
        [HttpGet]
        public EspLav Get(int idEspLav){
            return dm.GetEsperienza(idEspLav);
        }

        [Route("api/CV/{idCV}/Add/EspLav")]
        [HttpPost]
        public void Post(string idCV,[FromBody]EspLav EspLav){
            dm.AddEspLav(idCV,EspLav);
        }

        [Route("api/EspLav/Put/{idEspLav}")]
        [HttpPut]
        public void Put(int idEspLav, [FromBody] EspLav el){
            dm.ModEspLav(idEspLav,el);
        }
        
        [Route("api/EspLav/Del/{idEspLav}")]
        [HttpDelete]
        public void Delete(int idEspLav){
            dm.DelEspLav(idEspLav);
        }

    }
}
