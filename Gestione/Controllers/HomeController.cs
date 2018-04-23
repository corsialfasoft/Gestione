using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    public class Profilo {
        public string Matricola { get; set; }
        public string Ruolo { get; set; }
        public List<String> Funzioni { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public Profilo(string matricola, string ruolo, List<String> funzioni, string nome, string cognome) {
            Matricola = matricola;
            Ruolo = ruolo;
            Funzioni = funzioni;
            Nome = nome;
            Cognome = cognome;
        }
        public Profilo(){ }
    }
    public partial class HomeController : Controller {
        Profilo P = new Profilo();
		DomainModel dm = new DomainModel();
		// Profilo P;
        public HomeController() {
            P = new Profilo{Matricola="BBBB"};//ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
		}
        public ActionResult ListaCurriculum(){
			return View();
        }
		public ActionResult Index() {
            return View ();
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

        public CV InitForseCV(string nome,string cognome,int eta,string email,string residenza,string telefono) {
            CV cv = new CV();
            cv.Nome = nome;
            cv.Cognome = cognome;
            cv.Eta = eta;
            cv.Email = email;
            cv.Residenza = residenza;
            cv.Telefono = telefono;
            return cv;
			// Potremo aggiungere la matricola da qui
        }

        [HttpGet]
        public ActionResult DettCv(string id){
            DomainModel dm = new DomainModel();
            ViewBag.CV = dm.Search(id);
            return View("DettaglioCurriculum");
        }

       // [HttpPost] Commentato per risolvere bug su elimina da lista
        public ActionResult EliminaCV(string id){ 
            CV temp = dm.Search(id); 
            string prossimo ;
            try{ 
                dm.EliminaCV(temp);
                ViewBag.Message = "Curriculum eliminato con successo";
				Profilo P = new Profilo();
				P.Ruolo="admin"; // ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
                 if(P.Ruolo=="admin"){ 
                    prossimo = "RicercaCurriculum";
                }else{
                    prossimo = "MyPage";  
                }
            }catch(Exception){ 
                ViewBag.Message = "Non siamo riusciti a eliminare il curriculum selezionato"; 
                prossimo = "MyPage";
            }
            return View(prossimo);
        }

        [HttpPost]
        public ActionResult ModificaCV(string nome,string cognome,int eta,string email,string residenza,string telefono) {
            try{
               if(Session["profile"]!=null){ //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
                 //string matr = (Session["profile"] as Profilo).Matricola;//ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
					CV c = InitForseCV(nome,cognome,eta,email,residenza,telefono);
					c.Matricola=(Session["profile"] as Profilo).Matricola;
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

        [HttpPost]
        public ActionResult PassaEspLav(int annoInizioEsp,int annoFineEsp,string qualifica,string descrizioneEsp) {
            ViewBag.Esperienza = InitEspLav(annoInizioEsp,annoFineEsp,qualifica,descrizioneEsp);
            Session["esperienza"] = ViewBag.Esperienza;
            return View("ModEspLav");
        }

		[HttpPost]
		public ActionResult PassaComp(string tipo, int livello){
			ViewBag.Comp = InitComp(tipo, livello);
            Session["competenza"] = ViewBag.Comp;
			return View("ModComp");
		}
		[HttpPost]
		public ActionResult ModificaCompetenza(string tipo , int livello){
			Competenza c = new Competenza();
			c = InitComp(tipo,livello);
			Competenza daMod = Session["competenza"] as Competenza;
			string matr = (Session["profile"] as Profilo).Matricola;//ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
			dm.ModComp(daMod,c,matr);
			ViewBag.Comp = c;
			return View ("MyPage");
		}

		private Competenza InitComp(string tipo,int livello) {
			Competenza c = new Competenza();
			c.Titolo=tipo;
			c.Livello=livello;
			return c;
		}

		private EspLav InitEspLav(int annoInizioEsp,int annoFineEsp,string qualifica,string descrizioneEsp) {
            EspLav esp = new EspLav();
            esp.AnnoInizio = annoInizioEsp;
            esp.AnnoFine = annoFineEsp;
            esp.Qualifica = qualifica;
            esp.Descrizione = descrizioneEsp;
            return esp;
        }
        [HttpPost]
        public ActionResult PassaPerStud(int annoInizio,int annoFine,string titolo,string descrizione) {
            ViewBag.Percorso = InitPercorso(annoInizio,annoFine,titolo,descrizione);
            Session["percorso"] = ViewBag.Percorso;
            return View("ModPerStud");
        }

        private PerStud InitPercorso(int annoInizio,int annoFine,string titolo,string descrizione) {
            PerStud percorso = new PerStud();
            percorso.AnnoInizio = annoInizio;
            percorso.AnnoFine = annoFine;
            percorso.Titolo = titolo;
            percorso.Descrizione = descrizione;
            return percorso;
        }

        [HttpPost]
        public ActionResult PassaCV(string nome,string cognome,int eta,string email,string residenza,string telefono){
            ViewBag.CV = InitForseCV(nome,cognome,eta,email,residenza,telefono);
            return View("ModAnag");
        }

        //Da eliminare
        private CV InitCV(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello) {
                try{
				    CV cv = new CV {
					    Nome = nome,
					    Cognome = cognome,
					    Eta = int.Parse(eta),
					    Email = email,
					    Residenza = residenza,
					    Telefono = telefono,
                        Matricola="BBBB" //ATTENZIONE DA RIVEDERE QUANDO CI SARA' LA PROFILATURA
				    };
				    EspLav esp = new EspLav {
					    AnnoInizio = int.Parse(annoinizioesp),
					    AnnoFine = int.Parse(annofinesp),
					    Qualifica = qualifica,
					    Descrizione = descrizionesp
				    };
				    cv.Esperienze.Add(esp);
				    PerStud percorso = new PerStud {
					    AnnoInizio = int.Parse(annoinizio),
					    AnnoFine = int.Parse(annofine),
					    Titolo = titolo,
					    Descrizione = descrizione
				    };
				    cv.Percorsostudi.Add(percorso);
				    Competenza comp = new Competenza {
					    Titolo = tipo,
					    Livello = int.Parse(livello)
				    };
				    cv.Competenze.Add(comp);
                    return cv;
            } catch(Exception e) {
                throw e;
            }
        }
        
        [HttpPost]
        public ActionResult AggiungiCurriculum(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello
            ) {
            try{
                if (!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(cognome)
                    && !String.IsNullOrEmpty(eta) && !String.IsNullOrEmpty(email)
                    && !String.IsNullOrEmpty(telefono) && !String.IsNullOrEmpty(residenza)){
                    if(int.TryParse(eta, out int Eta)){
                        dm.AggiungiCV(InitCV(nome,cognome,eta,email,residenza,telefono,annoinizio,
                            annofine,titolo,descrizione,annoinizioesp,annofinesp,qualifica,descrizionesp,tipo,livello));
                        ViewBag.Message = "Curriculum Aggiunto";
                        return View("MyPage");
                    } else {
                        ViewBag.Message = "Eta' non valida";
                        return View("DettaglioCurriculum");
                    }
                } else{
                    ViewBag.Message = "Campi obbligatori da inserire...";
                    return View("DettaglioCurriculum");
                }
            } catch(Exception) {
                ViewBag.Message = "Qualcosa è andato storto";
                return View("DettaglioCurriculum");
            }
        }
		public void EliminaCompetenza(string titolo,int livello,string matricola)
		{
			dm.DelCompetenza(new Competenza {Titolo=titolo,Livello=livello },matricola);
			Response.Redirect($"/Home/DettCV/{matricola}");
		}
		public ActionResult EliminaPerStud(int AI ,int AF , string Ti , string Des){	
			PerStud ps = new PerStud();
			ps.AnnoInizio=AI;
			ps.AnnoFine=AF;
			ps.Titolo=Ti;
			ps.Descrizione=Des;
			dm.DelPerStud(ps,P.Matricola);
			ViewBag.CV = dm.Search(P.Matricola);
			return View("DettaglioCurriculum");
		}

    }
}