using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public partial class GetimeController : ApiController {
        // GET api/<controller>
        DomainModel dm = new DomainModel();

        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id) {
            return "value";
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

        [HttpGet] [Route ("api/Getime")]
        public IEnumerable<int> GetAnni () {
            return dm.Years("MkMatric");
        }

        [HttpGet] [Route ("api/Getime/{anno}")]
        public IEnumerable<int> GetMesi (int anno) {
            return dm.Month(anno, "MkMatric");
        }

        [HttpGet] [Route ("api/Getime/{anno}/{mese}")]
        public IEnumerable<DTGiornoDMese> GetMese (int anno, int mese) {
            return dm.DettaglioMese(anno, mese, "MkMatric");
        }

        [HttpGet] [Route ("api/Getime/{anno}/{mese}/{giorno}")]
        public DTGGiorno GetGiorno (int anno, int mese, int giorno) {
            return dm.VisualizzaGiorno(new DateTime(anno, mese, giorno), "MkMatric");
        }

        [HttpGet] [Route ("api/Getime/{anno}/{mese}/{giorno}/OreLavorative")]
        public IEnumerable<OreLavorate> GetCommessaGiorno (int anno, int mese, int giorno) {
            DTGGiorno dtg = dm.VisualizzaGiorno(new DateTime(anno, mese, giorno), "MkMatric");
            return dtg.OreLavorate;
        }
    }
}