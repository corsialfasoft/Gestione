using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public partial class HomeController : Controller {
		private Profilo profile;
		private const string FunctionRicercaCurriculum = "RicercaCurriculum";

		public ActionResult ListaCurriculum(){
			return View();
        }
        public ActionResult DettaglioCurriculum(){
            return View();
        }
        public ActionResult ModAnag() {
            return View();
        }
        public ActionResult ModEspLav() {
            return View();
        }
		public ActionResult ModComp(){
			return View();
		}
        public ActionResult ModPerStud() {
            return View();
        }
        [HttpPost]
        public ActionResult ModPerStud(int annoInizio, int annoFine, string titolo, string descrizione) {
            if(annoFine> annoInizio && titolo.Length>0 && descrizione.Length > 0){			    
                PerStud perSN = new PerStud { AnnoInizio = annoInizio, AnnoFine= annoFine,Titolo= titolo,Descrizione= descrizione };
                PerStud perSV = Session["percorso"] as PerStud;
                dm.ModPerStudi(profile.Matricola, perSV, perSN);
                ViewBag.Message = "Il percorso studi è stato modificato con successo, corri a controllare!";
            }else{
                ViewBag.Message ="Formato inserito non corretto";
                return View("MyPage");
            }
            return View($"MyPage");
        }
        [HttpPost]
        public ActionResult AddPerStud(string annoinizio, string annofine, string titolo, string descrizione) {
            int annoFine = int.Parse(annofine);
            int annoInizio = int.Parse(annoinizio);
            if (annoFine > annoInizio && titolo.Length > 0 && descrizione.Length > 0) {                
                PerStud perS = new PerStud { AnnoInizio = annoInizio, AnnoFine = annoFine, Titolo = titolo, Descrizione = descrizione };
                dm.AddCvStudi(profile.Matricola, perS);
                ViewBag.Message="Il percorso studi è stato inserito con successo nel tuo Curriculum!";
				ViewBag.CV = dm.Search(profile.Matricola);
				ModelState.Clear();
				return View("DettaglioCurriculum");
			} else{
                ViewBag.Message = "Formato inserito non corretto";
                return View("MyPage");
            }
        }
		 [HttpPost]
        public ActionResult ModEspLav(int annoInizioEsp, int annoFineEsp, string qualifica, string descrizioneEsp){
            EspLav esp = new EspLav{ AnnoInizio=annoInizioEsp,AnnoFine=annoFineEsp,Qualifica=qualifica,Descrizione=descrizioneEsp};
            EspLav espV = Session["esperienza"] as EspLav;
            dm.ModEspLav(profile.Matricola,espV,esp);
            ViewBag.Message = "Funziona";
            return View($"MyPage");
        }
        public ActionResult AddEspLav(int annoinizioesp, int annofinesp, string qualifica, string descrizionesp){
            EspLav esp = new EspLav{ AnnoInizio=annoinizioesp,AnnoFine= annofinesp,Qualifica=qualifica,Descrizione=descrizionesp};
            dm.AddEspLav(profile.Matricola,esp);
            ViewBag.Message="Esperienza aggiunta nel curriculum,corri a controllare!";
			ViewBag.CV = dm.Search(profile.Matricola);
			ModelState.Clear();
			return View("DettaglioCurriculum");
        }
		 [HttpPost]
        public ActionResult AddComp(string tipo,string livello){
			Competenza comp = new Competenza {
				Titolo = tipo,
				Livello = int.Parse(livello)
			};
			dm.AddCompetenze(profile.Matricola,comp);
            ViewBag.CV = dm.Search(profile.Matricola);
			ModelState.Clear();
			return View("DettaglioCurriculum");
        }
		[HttpGet]
        public ActionResult DettCv(string id){           
            ViewBag.CV = dm.Search(id);
            return View("DettaglioCurriculum");
        }
		[HttpPost]
        public ActionResult PassaEspLav(int annoInizioEsp,int annoFineEsp,string qualifica,string descrizioneEsp) {
            ViewBag.Esperienza = InitEspLav(annoInizioEsp,annoFineEsp,qualifica,descrizioneEsp);
            ViewBag.Matricola = profile.Matricola;
			Session["esperienza"] = ViewBag.Esperienza;
            return View("ModEspLav");
        }
		[HttpPost]
		public ActionResult PassaComp(string tipo, int livello){
			ViewBag.Matricola = profile.Matricola;
			ViewBag.Comp = InitComp(tipo, livello);
            Session["competenza"] = ViewBag.Comp;
			return View("ModComp");
		}
		[HttpPost]
		public ActionResult ModificaCompetenza(string tipo , int livello){
			Competenza c = new Competenza();
			c = InitComp(tipo,livello);
			Competenza daMod = Session["competenza"] as Competenza;
			dm.ModComp(daMod,c,profile.Matricola);
			ViewBag.Comp = c;
			return View ("MyPage");
		}
		 [HttpPost]
        public ActionResult PassaPerStud(int annoInizio,int annoFine,string titolo,string descrizione) {
            ViewBag.Percorso = InitPercorso(annoInizio,annoFine,titolo,descrizione);
			ViewBag.Matricola = profile.Matricola;
            Session["percorso"] = ViewBag.Percorso;
            return View("ModPerStud");
        }
		[HttpPost]
        public ActionResult PassaCV(string nome,string cognome,int eta,string email,string residenza,string telefono){
            ViewBag.CV = InitForseCV(nome,cognome,eta,email,residenza,telefono);
			ViewBag.CV.Matricola = profile.Matricola;
            return View("ModAnag");

        }
		[HttpPost]
       public ActionResult ModificaCV(string nome,string cognome,int eta,string email,string residenza,string telefono) {
			try{
              if(profile.Matricola!=null){ //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
					CV c = InitForseCV(nome,cognome,eta,email,residenza,telefono);
					c.Matricola=profile.Matricola;
                    dm.ModificaCV(c);   
                    ViewBag.Message = "Dati anagrafici modificati";
					ViewBag.CV = c;
                    return View("MyPage");
                }
            }catch(Exception){ 
                ViewBag.Message = "Si è verificato un errore, non siamo riusciti a modificare i dati anagrafici";    
            }
            return View("ModAnag");
		}
        public ActionResult EliminaCV(string id){ 
            CV temp = dm.Search(id); 
            string prossimo ;
            try{ 
                dm.EliminaCV(temp);
                ViewBag.Message = "Curriculum eliminato con successo";

				if(profile.CheckFunction(FunctionRicercaCurriculum)){ //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
                    prossimo = FunctionRicercaCurriculum;
                }else{
                    prossimo = "MyPage";  
                }
            }catch(Exception){ 
                ViewBag.Message = "Non siamo riusciti a eliminare il curriculum selezionato"; 
                prossimo = "MyPage";
            }
            return View(prossimo);
        }
		public ActionResult MyPage(){
            ViewBag.Profilo = profile;
			return View();
		}
		[HttpPost]
		public ActionResult MyPage(string id){
			//Profilo P = profile;
			if (id == profile.Matricola || id == null) {
				id = profile.Matricola;
			}
			CV trovato = dm.Search(id);
			if (trovato == null) {
				ViewBag.Message = $"Non è stato trovato alcun Curriculum con questo codice";
				return View();
			}
			ViewBag.CV = trovato;
			return View("DettaglioCurriculum");
		}
		public ActionResult RicercaCurriculum(){
			return View();
		}
		[HttpPost]
		public ActionResult RicercaCurriculum(string chiava,string eta,string etaMin,string etaMax,string cognome){
			List<CV> trovati = new List<CV>();
			if (chiava != "") {
				trovati = dm.SearchChiava(chiava);
				if (trovati.Count > 0) {
					ViewBag.ListaCV= trovati;
					return View("ListaCurriculum");
				}
				ViewBag.Message="Non è stato trovato nessun elemento";
				return View();
			}else if(eta != "" && int.TryParse(eta,out int codice)) {
				trovati = dm.SearchEta(codice);
				if (trovati.Count > 0) {
					ViewBag.ListaCV= trovati;
					return View("ListaCurriculum");
				}
				ViewBag.Message="Non è stato trovato nessun elemento";
				return View();
			}else if(etaMin!= "" && etaMax!="" && int.TryParse(etaMin,out int etaMinima) && int.TryParse(etaMax,out int etaMassima)) {
				if(etaMassima < etaMinima) {
					ViewBag.Message="L'età massima non può essere minore dell'età minima";
					return View();
				}else if (etaMassima == etaMinima) {
					ViewBag.Message="Età minima e massima sono uguali";
					return View();
				}
				trovati = dm.SearchRange(etaMinima,etaMassima);
				if (trovati.Count > 0) {
					ViewBag.ListaCV= trovati;
					return View("ListaCurriculum");
				}
				ViewBag.Message="Non è stato trovato nessun elemento";
				return View();
			}else if(cognome!="") {
				trovati = dm.SearchCognome(cognome);
				if(trovati.Count > 0) {
					ViewBag.ListaCV=trovati;
					return View("ListaCurriculum");
				} else {
					ViewBag.Message="Non è stato trovato nessun elemento";
					return View();
				}
			}
			ViewBag.Message="Inserire dei parametri di ricerca validi";
			return View();
		}
		public void EliminaEsperienza(int annoInizioEsp, int annoFineEsp, string qualifica, string descrizioneEsp,string matricola){
			dm.DelEspLav(new EspLav{AnnoInizio=annoInizioEsp,AnnoFine=annoFineEsp,Qualifica=qualifica,Descrizione=descrizioneEsp },matricola);
			 Profilo p = profile;
			Response.Redirect($"/Home/DettCv/{matricola}");
		}
		public void EliminaCompetenza(string titolo,int livello,string matricola){
			dm.DelCompetenza(new Competenza {Titolo=titolo,Livello=livello },matricola);
			Response.Redirect($"/Home/DettCV/{matricola}");
		}
		public ActionResult EliminaPerStud(int AI ,int AF , string Ti , string Des){
			PerStud ps = new PerStud {
				AnnoInizio = AI,
				AnnoFine = AF,
				Titolo = Ti,
				Descrizione = Des
			};

			dm.DelPerStud(ps,profile.Matricola);
			ViewBag.CV = dm.Search(profile.Matricola);
			return View("DettaglioCurriculum");
		}
    }
}