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
        Profilo profile = new Profilo();

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

        [HttpGet] [Route ("api/Getime/{anno}/{mese}/{giorno}/oreLavorative")]
        public IEnumerable<OreLavorate> GetCommessaGiorno (int anno, int mese, int giorno) {
            DTGGiorno dtg = dm.VisualizzaGiorno(new DateTime(anno, mese, giorno), "MkMatric");
            return dtg.OreLavorate;
        }
        // GET api/<controller>
        [HttpPost]
        [Route("api/Getime")]
        public IHttpActionResult AddGiorno(AddGiorno addGiorno) {
            if(addGiorno.Data.CompareTo(DateTime.Today) < 0 || addGiorno.Data.CompareTo(DateTime.Today.AddDays(-180))<0) {
                return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} non si puo inserire");
            }
            DTGGiorno giorno = dm.VisualizzaGiorno(addGiorno.Data, "MkMatric");
            try {
                int oreT = 0;
                int oreL = 0;
                if (giorno != null) {
                    oreT = giorno.OreMalattia + giorno.OrePermesso + giorno.TotOreLavorate + giorno.OreFerie;
                    if (giorno.OreFerie > 0) {
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} eri in ferie");
                    }
                    oreL = giorno.TotOreLavorate;
                }
                if (addGiorno.TipoOre == "Ore di lavoro") {
                    if (addGiorno.Commessa == "") {
                        return BadRequest("Inserire la commessa");
                    }
                    if (oreT == oreL && oreT + addGiorno.Ore > 14) {
                        return BadRequest("Massimo ore lavorative raggiunte!");
                    } else if (oreT != oreL && oreT + addGiorno.Ore > 8) {
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} stai superando le 8 ore");
                    }
                    DTCommessa commessa = dm.CercaCommessa(addGiorno.Commessa);
                    if (commessa == null) {
                        return BadRequest("Commessa non trovata");
                    } else {
                        if (commessa.OreLavorate + addGiorno.Ore > commessa.Capienza) {
                            return BadRequest($"Capienza ore commessa superate!\nMassimo ore: {commessa.Capienza}");
                        }
                        dm.CompilaHLavoro(addGiorno.Data, addGiorno.Ore, commessa.Id, "MkMatric");
                    }
                } else if (addGiorno.TipoOre == "Ore di permesso") {
                    if (oreT + addGiorno.Ore > 8)
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} stai superando le 8 ore");
                    dm.Compila(addGiorno.Data, addGiorno.Ore, (HType)2, "MkMatric");
                } else if (addGiorno.TipoOre == "Ore di malattia") {
                    if (oreT + addGiorno.Ore > 8)
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} stai superando le 8 ore");
                    dm.Compila(addGiorno.Data, addGiorno.Ore, (HType)1, "MkMatric");
                } else if (addGiorno.TipoOre == "Ore di ferie") {
                    if (oreT + 8 > 8)
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} stai superando le 8 ore");
                    dm.Compila(addGiorno.Data, 8, (HType)3, "MkMatric");
                } else {
                    return BadRequest($"Input Errato!");
                }
            } catch (Exception e) {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return StatusCode(HttpStatusCode.OK);
        }
    }
    public class AddGiorno {
        public DateTime Data { get; set; }
        public int Ore { get; set; }
        public string TipoOre { get; set; }
        public string Commessa { get; set; }
    }
}