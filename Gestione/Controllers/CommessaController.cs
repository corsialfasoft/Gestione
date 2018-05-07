using Gestione.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gestione.Controllers {
    public class CommesseController : ApiController {
        DomainModel dm = new DomainModel();
        // GET api/Commesse/nome
        [HttpGet]
        [Route("api/Commesse/{nome}")]
        public DTCommessa GetCommessa(string nome) {
            return dm.CercaCommessa(nome);
        }

        // GET api/Commesse
        [HttpGet]
        [Route("api/Commesse")]
        public IEnumerable<DTCommessa> Get() {
            return dm.CercaCommesse("");
        }
    }
}