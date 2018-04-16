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
        DomainModel dm = new DomainModel();
		// Profilo P;
        public HomeController() {
            //P = new Profilo{Matricola="801130"};
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
				P.Ruolo="admin"; // ATTENZIONE SETTATO SUL CONTROLLER!!!!
                 if(P.Ruolo=="admin"){ 
                    prossimo = "ListaCurriculum";
                }else{
                    prossimo = "MyPage";  
                }
            }catch(Exception){ 
                ViewBag.Message = "Non siamo riusciti a eliminare il curriculum selezionato"; 
                prossimo = "MyPage";
            }
            return View(prossimo);
        }

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
					Telefono = telefono
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

        [HttpPost]
        public ActionResult ModificaCurriculum(string nome,string cognome,string eta,
            string email,string residenza,string telefono,string annoinizio,string annofine,
            string titolo, string descrizione, string annoinizioesp, string annofinesp,string qualifica,
            string descrizionesp,string tipo,string livello,string id
            ) {
			Profilo P = new Profilo();
            try{
                if (!String.IsNullOrEmpty(nome) && !String.IsNullOrEmpty(cognome)
                    && !String.IsNullOrEmpty(eta) && !String.IsNullOrEmpty(email)
                    && !String.IsNullOrEmpty(telefono) && !String.IsNullOrEmpty(residenza)){
                    if(int.TryParse(eta, out int Eta)){
                        dm.ModificaCV(InitCV(ViewBag.CV.nome,ViewBag.CV.cognome,
                            ViewBag.CV.eta,ViewBag.CV.email,ViewBag.CV.residenza,ViewBag.CV.telefono,ViewBag.CV.annoinizio,
                            ViewBag.CV.annofine,ViewBag.CV.titolo,ViewBag.CV.descrizione,ViewBag.CV.annoinizioesp,
                            ViewBag.CV.annofinesp,ViewBag.CV.qualifica,ViewBag.CV.descrizionesp,ViewBag.CV.tipo,ViewBag.CV.livello),
                            InitCV(nome,cognome,eta,email,residenza,telefono,annoinizio,annofine,titolo,descrizione,
                            annoinizioesp,annofinesp,qualifica,descrizionesp,tipo,livello));
                        ViewBag.Message = "Curriculum Modificato";
                        return View("MyPage");
                    } else {
		        	    CV trovato = dm.Search(P.Matricola);
                         ViewBag.CV = trovato;
                        ViewBag.Message = "Eta' non valida";
                        return View("DettaglioCurriculum");
                    }
                } else {
			        CV trovato = dm.Search(P.Matricola);
                    ViewBag.CV = trovato;
                    ViewBag.Message = "Campi obbligatori da inserire...";
                    return View("DettaglioCurriculum");
                }
            } catch(Exception) {
			    CV trovato = dm.Search(P.Matricola);
                ViewBag.CV = trovato;
                ViewBag.Message = "Qualcosa è andato storto.";
                return View("DettaglioCurriculum");
            }
        }
    }
}