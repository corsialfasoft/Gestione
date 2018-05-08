using Gestione.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Gestione.Controllers {
    public class CommesseController : ApiController {
        DomainModel dm = new DomainModel();
        // GET api/Commesse/nome
        [HttpGet]
        [Route("api/Commesse/{nome}")]
        public IEnumerable<string> GetCommesse(string nome) {
            return dm.CercaCommesse(nome).ConvertAll(new Converter<DTCommessa, string>(commessa => commessa.Nome));
        }
        [HttpGet]
        [Route("api/Commesse/nome/{nome}")]
        public DTCommessa GetCommessa(string nome) {
            return dm.CercaCommessa(nome);
        }

        // GET api/Commesse
        [HttpGet]
        [Route("api/Commesse")]
        public IEnumerable<string> Get() {
            return dm.CercaCommesse("").ConvertAll(new Converter<DTCommessa, string>(commessa => commessa.Nome));
        }
    }
}