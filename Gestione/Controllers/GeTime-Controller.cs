using Gestione.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestione.Controllers {
	public partial class HomeController{
        DomainModel dm = new DomainModel();
 
		public ActionResult VisualizzaGiorno() {
            return View();
        }
        [HttpPost]
        public ActionResult VisualizzaGiorno(DateTime data) {
            DTGGiorno giorno = dm.VisualizzaGiorno(data, profile.Matricola);
            if (giorno!=null) {
                ViewBag.giorno = giorno;
            } else {
                ViewBag.Message = "Data non trovata!";
            }
            return View();
        }
		public ActionResult VisualizzaCommessa() {
			return View("VisualizzaCommessa");
		}
		[HttpPost]
		public ActionResult VisualizzaCommessa(string commessa) {
			if(commessa.Length==0)
				ViewBag.Message = "Inserire il nome della commessa!";
			else{ 
				try{
					List<DTCommessa> dTCommesse = dm.CercaCommesse(commessa);
					if(dTCommesse.Count>0) {
                        if (dTCommesse.Count==1) {
                            List<DTGiorno> giorni = dm.GiorniCommessa(dTCommesse[0].Id,profile.Matricola);
						    ViewBag.NomeCommessa= dTCommesse[0].Nome;
                            ViewBag.Commesse = dTCommesse;
							ViewBag.Giorni = giorni;
                        } else {
                            ViewBag.Commesse = dTCommesse;
                        }
                    } else {
						ViewBag.Message = "Non è stato trovata nessuna commessa con questo nome";
                    }
				} catch(Exception) {
					ViewBag.Message = "Errore del server";
				}
			}
			return View("VisualizzaCommessa");
		}
        [HttpGet]
        public ActionResult DettaglioCommessa(string nome) {
            ViewBag.Commesse = VisualizzaCommessa(nome);
            return View("VisualizzaCommessa");
        }
        public ActionResult GeTimeHome() {
            return View();
        }
		public ActionResult AddGiorno() {
			return View();
		}

		[HttpPost]
		public ActionResult AddGiorno(DateTime dateTime, string tipoOre, int? ore, string Commessa) {
            if(tipoOre==""){
                ViewBag.Message="Scegliere la tipologia delle ore";
                return View();
            }
            if (ore == null && tipoOre != "Ore di ferie" || (ore != null && ore <= 0)) {
                ViewBag.Message = "Inserire le ore";
                return View();
            }
            DTGGiorno giorno = dm.VisualizzaGiorno(dateTime, profile.Matricola);
			try{
                int oreT =0;
                int oreL = 0;
                if (giorno != null && (giorno.data.CompareTo(DateTime.Today) <= 0 || giorno.data.Month >= (DateTime.Now.Month - 6))) {
                    oreT = giorno.OreMalattia + giorno.OrePermesso + giorno.TotOreLavorate + giorno.OreFerie;
                    if (giorno.OreFerie > 0) {
                        ViewBag.Giorno = giorno;
                        ViewBag.Message = $"Il giorno {dateTime.ToString("yyyy-MM-dd")} eri in ferie";
                        return View();
                    }
                    oreL= giorno.TotOreLavorate;
                }
				if (tipoOre == "Ore di lavoro"){
                    if(Commessa == ""){
                        ViewBag.Message="Inserire la commessa";
                        return View();
                    }
                    if (oreT == oreL && oreT + ore > 14) {
                        ViewBag.Giorno = giorno;
                        ViewBag.Message="Massimo ore lavorative raggiunte!";
                        return View();
                    } else if (oreT != oreL && oreT + ore > 8)
                        return ErrorMessage(dateTime, giorno);
                    List<DTCommessa> commesse = dm.CercaCommesse(Commessa);
					if (commesse.Count == 0){
						ViewBag.Message ="Commessa non trovata";
						return View();
					} else if(commesse.Count == 1){
                        if (commesse[0].OreLavorate+ore>commesse[0].Capienza) {
                            ViewBag.Message = $"Capienza ore commessa superate!\nMassimo ore: {commesse[0].Capienza}";
                            return View();
                        }
						dm.CompilaHLavoro(dateTime,(int) ore, commesse[0].Id, profile.Matricola);				
					} else if(commesse.Count > 1) {
                        Session["stateGiorno"] = new StateGiorno { Data= dateTime, Ore=(int)ore };
                        ViewBag.ListaCommesse = commesse;
                        return View();
					}
				} else if (tipoOre == "Ore di permesso"){
                    if (oreT + ore > 8)
                        return ErrorMessage(dateTime, giorno);
                    dm.Compila(dateTime,(int)ore, (HType)2, profile.Matricola);
				} else if (tipoOre == "Ore di malattia") {
                    if (oreT + ore > 8) 
                        return ErrorMessage(dateTime, giorno);
                    dm.Compila(dateTime, (int)ore, (HType)1, profile.Matricola);
				} else if (tipoOre == "Ore di ferie"){
                    if (oreT + 8 > 8) 
                        return ErrorMessage(dateTime, giorno);
                    dm.Compila(dateTime, 8, (HType)3, profile.Matricola);
                } else {
                    ViewBag.Message = $"Input Errato!";
                    return View();
                }
				ViewBag.EsitoAddGiorno = ore + " " + tipoOre + " aggiunte!";
                ViewBag.GeCoDataTime = dateTime;
            } catch(Exception e){
                ViewBag.Message = "Errore server";
            }
			return View();
		}

        private ActionResult ErrorMessage(DateTime dateTime, DTGGiorno giorno) {
            ViewBag.Giorno = giorno;
            ViewBag.Message = $"Il giorno {dateTime.ToString("yyyy-MM-dd")} stai superando le 8 ore";
            return View("AddGiorno");
        }

        public ActionResult Modifica() {
            return View();
        }
        [HttpPost]
        public ViewResult VisualizzaMese(string anno, string mese = "1") {
            if (anno != "" && int.TryParse(anno, out int annoI) && int.TryParse(mese, out int meseI)) {
                ViewBag.Mese = dm.DettaglioMese(annoI, meseI, profile.Matricola);
                if (ViewBag.Mese.Count > 0) {
                    ViewBag.TOreL = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.TotOreLavorate);
                    ViewBag.TOreP = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.OrePermesso);
                    ViewBag.TOreM = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.OreMalattia);
                    ViewBag.TOreF = ((List<DTGiornoDMese>)ViewBag.Mese).Sum<DTGiornoDMese>(dTGiornoDMese => dTGiornoDMese.OreFerie);
                }
                ViewBag.Year = annoI;
                ViewBag.Month = meseI;
            } else
                ViewBag.Message = "Inserire anno e mese";
            return View();
        }
        public ViewResult VisualizzaMese() {

            return View();
        }
        [HttpGet]
        public ViewResult AddGiornoSelectCommessa(string nome) {
            try { 
                StateGiorno stateGiorno = Session["stateGiorno"] as StateGiorno;
                if (stateGiorno != null && nome != "") {
                    DTCommessa commessa = dm.CercaCommessa(nome);
                    if (commessa!=null) {
                        dm.CompilaHLavoro(stateGiorno.Data, stateGiorno.Ore, commessa.Id, profile.Matricola);
                        ViewBag.GeCoDataTime = stateGiorno.Data;
                        ViewBag.EsitoAddGiorno = stateGiorno.Ore + " ore di lavoro aggiunte!";
                        Session["stateGiorno"] = null;
                    } else {
                        ViewBag.Message = "Operazione non consentita";
                    }
                } else
                    ViewBag.Message = "Operazione non consentita";
            }catch(Exception e) {
                ViewBag.Message = e.Message;
            }
            return View("AddGiorno");
        }
        public class StateGiorno {
            public DateTime Data { get; set; }
            public int Ore { get; set; }
        }
    }
}