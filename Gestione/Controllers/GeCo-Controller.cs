using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces;
using Gestione.Models;

namespace Gestione.Controllers
{

	
	public partial class HomeController : Controller
	{
		//Profilo P;
		//TODO Remove
        //public HomeController(Profilo p) {
        //    P = p;
        //}
        public HomeController(){
			profile = ProfileMock.Instance(Session).GetProfile();
		}
		public ActionResult ElencoCorsi()
		{
			ViewBag.Controllo = null;
			List<Corso> ris = dm.ListaCorsi();
			if(ris != null) {
				ViewBag.Corsi = ris;
			} else {
				ViewBag.Message = "Elenco vuoto";
			}
			return View();
		}
		[HttpPost]
		public ActionResult ElencoCorsi(bool mieiCorsi,string descrizione, string id)
		{
			if(mieiCorsi){
				if(int.TryParse(id, out int output)){
					Corso c = dm.SearchCorsi(output);
					if(c != null){
						ViewBag.Controllo = true;
                        ViewBag.Corso = c;
						ViewBag.Lezioni = dm.ListaLezioni(c);
                        return View("Corso");
					}else{
						ViewBag.Controllo = false;
						ViewBag.Message="Corso non trovato!";
						return View("ElencoCorsi");
					}
				}else if (descrizione.Length >0){
					List<Corso> corsos = dm.SearchCorsi(descrizione, profile.Matricola);
					if(corsos.Count >0 ){
						ViewBag.Controllo = true;
                        ViewBag.Corsi = corsos;
						return View("ElencoCorsi");
					}else{
						ViewBag.Controllo = false;
						ViewBag.Message="Corsi non trovati!";
						return View("ElencoCorsi");
					}
				}else{
					ViewBag.Controllo = false;
					ViewBag.Message="Input errato!!";
					return View("ElencoCorsi");
				}
			}else{
			if(int.TryParse(id, out int output)){
					Corso c = dm.SearchCorsi(output);
					if(c != null){
						ViewBag.Controllo = true;
                        ViewBag.Corso = c;
						ViewBag.Lezioni = dm.ListaLezioni(c);
						return View("Corso");
					}else{
						ViewBag.Controllo = false;
						ViewBag.Message="Corso non trovato!";
						return View("ElencoCorsi");
					}
				}else if (descrizione.Length >0){
					List<Corso> corsos = dm.SearchCorsi(descrizione);
					if(corsos.Count >0 ){
						ViewBag.Controllo = true;
                        ViewBag.Corsi = corsos;
						return View("ElencoCorsi");
					}else{
						ViewBag.Controllo = false;
						ViewBag.Message="Corsi non trovati!";
						return View("ElencoCorsi");
					}
				}else{
					ViewBag.Controllo = false;
					ViewBag.Message="Input errato!!";
					return View("ElencoCorsi");
				}
			}			
		}
		public ActionResult ElencoCorso(int id)
		{
			Corso c = dm.SearchCorsi(id);
			List<Corso> res = new List<Corso> { c };
			ViewBag.Corsi = res;
			return View("ElencoCorsi");
		}
		[HttpPost]
		public ActionResult AddCorso(string _nome,string _descrizione,DateTime _inizio,DateTime _fine)
		{
			string prossimo;
			Corso temp = new Corso { Nome = _nome,Descrizione = _descrizione,Inizio = _inizio,Fine = _fine };
			try {
				dm.AddCorso(temp);
				ViewBag.Message = "Corso inserito correttamente";
				prossimo = "AddCorso";
			} catch(Exception) {
				ViewBag.Message = "Qualcosa è andato storto";
				prossimo = "AddCorso";
			}
			return View(prossimo);
		}
		public ActionResult AddCorso()
		{
			return View();
		}
		[HttpPost]
		public ActionResult AddLezione(string LezNome,string LezDescrizione,int LezDurata,int id)
		{
			Lezione lez = new Lezione {
				Nome = LezNome,
				Descrizione = LezDescrizione,
				Durata = LezDurata
			};
			dm.AddLezione(id,lez);
			ViewBag.CorsoId = id;
			ViewBag.Message = "Lezione aggiunta correttamente";
			return View();
		}
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult AddLezione(int id)
		{
			ViewBag.Message = id;
			ViewBag.CorsoId = id;
			return View();
		}
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";
			return View();
		}
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";
			return View();
		}
		public ActionResult ElencoCorsiStudente(string matricola)
		{
			List<Corso> corso = dm.ListaCorsi(matricola);
			ViewBag.CorsiStudente = corso;
			return View();
		}
		public ActionResult Corso(int id)
		{
			Corso scelto = dm.SearchCorsi(id);
			List<Lezione> lezions = dm.ListaLezioni(scelto);
			ViewBag.Corso = scelto;
			ViewBag.Lezioni = lezions;
			return View();
		}
		public ActionResult Iscrizione(int id){
			try {
				dm.Iscriviti(id,profile.Matricola);
				ViewBag.Message = "Iscrizione andata a buon fine";
				ViewBag.Corsi = dm.ListaCorsi();
			} catch(Exception e) {
				ViewBag.Message = e.Message;
			}
			return View("ElencoCorsi");
		}
		public ActionResult ModificaLezione(string nomeLezione,int idLezione,string descrizioneLezione,int durataLezione,int id)
		{
			Lezione a = new Lezione {
				Nome = nomeLezione,
				Id = idLezione,
				Descrizione = descrizioneLezione,
				Durata = durataLezione
			};
			ViewBag.Lezione = a;
			Corso c =dm.SearchCorsi(id);
			ViewBag.Corso =c;
			ViewBag.Corso.Id = c.Id;
			return View();
		}
		[HttpPost]
		public ActionResult ModificaLezionePost(string LezNome,string LezDescrizione,int LezDurata,int idLezione,int id)
		{
			Lezione lezione = new Lezione {
				Id = idLezione,
				Nome = LezNome,
				Descrizione = LezDescrizione,
				Durata = LezDurata
			};
			try {
				dm.ModLezione(lezione);
			} catch(Exception) {
				ViewBag.Message = "Qualcosa è andato storto.";
			}
			Corso s = dm.SearchCorsi(id);			
			ViewBag.Corso=s;
			ViewBag.Lezioni = dm.ListaLezioni(s);
			return View("Corso");
		}
        public ActionResult ModificaCorso(int idCorso) {
            Corso Cor = dm.SearchCorsi(idCorso);
            if (Cor == null) { 
                ViewBag.Message="Corso non trovato";
                return View("ElencoCorsi");
            }
            ViewBag.Corso=Cor;
            return View();

        }
        [HttpPost]
        public ActionResult ModificaCorso(string CorNome, string CorDescrizione, DateTime CorInizio, DateTime CorFine, int idCorso) {
            if(CorNome.Length>0 && CorDescrizione.Length > 0) { 
                Corso Corso = new Corso {
                    Id = idCorso,
                    Nome = CorNome,
                    Descrizione = CorDescrizione,
                    Inizio = CorInizio,
                    Fine = CorFine
                };
                try {
                    dm.ModCorso(Corso);
                } catch (Exception) {
                    ViewBag.Message = "Qualcosa è andato storto.";
                }         
            }
            Corso s = dm.SearchCorsi(idCorso);
            ViewBag.Corso = s;
            ViewBag.Lezioni = dm.ListaLezioni(s);
            return View("Corso");
        }
    }
}