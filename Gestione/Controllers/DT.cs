using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gestione.Models;
using Interfaces;

namespace Gestione.Controllers {
    
    public partial class HomeController : Controller {
        
        public CV InitForseCV(string nome,string cognome,int eta,string email,string residenza,string telefono) {
			CV cv = new CV {
				Nome = nome,
				Cognome = cognome,
				Eta = eta,
				Email = email,
				Residenza = residenza,
				Telefono = telefono
			};
			return cv;
        }
		private Competenza InitComp(string tipo,int livello) {
			Competenza c = new Competenza {
				Titolo = tipo,
				Livello = livello
			};
			return c;
		}
		private EspLav InitEspLav(int annoInizioEsp,int annoFineEsp,string qualifica,string descrizioneEsp) {
			EspLav esp = new EspLav {
				AnnoInizio = annoInizioEsp,
				AnnoFine = annoFineEsp,
				Qualifica = qualifica,
				Descrizione = descrizioneEsp
			};
			return esp;
        }
        private PerStud InitPercorso(int annoInizio,int annoFine,string titolo,string descrizione) {
			PerStud percorso = new PerStud {
				AnnoInizio = annoInizio,
				AnnoFine = annoFine,
				Titolo = titolo,
				Descrizione = descrizione
			};
			return percorso;
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
					Telefono = telefono,
                    Matricola="BBBB"
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

    }
    public class DTGiorno {
        public DateTime Data { get; set; }
        public int OreLavorate { get; set; }
    }
    public class DTCommessa {
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public string Nome { get; set; }
        public int Capienza { get; set; }
        public int OreLavorate { get; set; }
        public DTCommessa(int id, string nome, string descrizione, int capienza, int oreLavorate) {
            Id = id;
            OreLavorate = oreLavorate;
            Nome = nome;
            Descrizione = descrizione;
            Capienza = capienza;
        }
    }
    public class DTGGiorno {
        public DateTime data { get; set; }
        public int TotOreLavorate { get; set; }
        public int OrePermesso { get; set; }
        public int OreMalattia { get; set; }
        public int OreFerie { get; set; }
        public DTGGiorno() { }
        private List<OreLavorate> OreLavorates = new List<OreLavorate>();
        public List<OreLavorate> OreLavorate { get { return OreLavorates; } }
    }
    public class OreLavorate {
        public string nome { get; set; }
        public int oreGiorno { get; set; }
        public string descrizione { get; set; }
    }
    public class DTGiornoDMese {
        public DateTime data { get; set; }
        public int TotOreLavorate { get; set; }
        public int OrePermesso { get; set; }
        public int OreMalattia { get; set; }
        public int OreFerie { get; set; }
        public override bool Equals(object obj) {
            return data.Equals(((DTGiornoDMese)obj).data);
        }
    }
}