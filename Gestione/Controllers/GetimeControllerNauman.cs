using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gestione.Controllers {
    public partial class GetimeController : ApiController {
        DomainModel dm = new DomainModel();
        Profilo profile = new Profilo();
        // GET api/<controller>
        [HttpPost]
        [Route("api/Getime")]
        public IHttpActionResult AddGiorno(AddGiorno addGiorno) {
            DTGGiorno giorno = dm.VisualizzaGiorno(addGiorno.Data, profile.Matricola);
            try {
                int oreT = 0;
                int oreL = 0;
                if (giorno != null && (giorno.data.CompareTo(DateTime.Today) <= 0 || giorno.data.Month >= (DateTime.Now.Month - 6))) {
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
                    if (commessa == null)  {
                        return BadRequest("Commessa non trovata");
                    }else { 
                        if (commessa.OreLavorate + addGiorno.Ore > commessa.Capienza) {
                            return BadRequest($"Capienza ore commessa superate!\nMassimo ore: {commessa.Capienza}");
                        }
                        dm.CompilaHLavoro(addGiorno.Data, addGiorno.Ore, commessa.Id, profile.Matricola);
                    } 
                } else if (addGiorno.TipoOre == "Ore di permesso") {
                    if (oreT + addGiorno.Ore > 8)
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} stai superando le 8 ore");
                    dm.Compila(addGiorno.Data, addGiorno.Ore, (HType)2, profile.Matricola);
                } else if (addGiorno.TipoOre == "Ore di malattia") {
                    if (oreT + addGiorno.Ore > 8)
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} stai superando le 8 ore");
                    dm.Compila(addGiorno.Data, addGiorno.Ore, (HType)1, profile.Matricola);
                } else if (addGiorno.TipoOre == "Ore di ferie") {
                    if (oreT + 8 > 8)
                        return BadRequest($"Il giorno {addGiorno.Data.ToString("yyyy-MM-dd")} stai superando le 8 ore");
                    dm.Compila(addGiorno.Data, 8, (HType)3, profile.Matricola);
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
        public DateTime Data{ get;set;}
        public int Ore{ get;set;}
        public string TipoOre { get; set;}
        public string Commessa { get; set;}
    }
}