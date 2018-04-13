using System;
using System.Collections.Generic;

namespace Interfaces{ 
	public interface IGeCV{
		void ModificaCV(CV a, CV b); //modifica un curriculum nel db
		void AggiungiCV(CV a); //quando sei loggato, puoi aggiungere un curriculum nel db
		void CaricaCV(string path); //quando non sei loggato, puoi spedire un curriuculum
		CV Search(string id); //search di un curriculum per id di un curriculum
		List<CV> SearchChiava(string chiava); //search generale per parole chiava 
		List<CV> SearchEta(int eta); //search solo per quella precisa età
		List<CV> SearchRange(int etmin, int etmax); //search per un range di età minimo e massimo
		void EliminaCV(CV curriculum); //Elimina un CV dal db
		List<CV> SearchCognome(string cognome); //Ricerca solo per cognome
	}
	public enum HType { HMalattia = 0, HPermesso, HFerie }
	interface IGeTime {
		void CompilaHLavoro(DateTime data, int ore, int idCommessa, int idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, int idUtente);
		Giorno VisualizzaGiorno(DateTime data, int idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, string idUtente);
		Commessa CercaCommessa(string nomeCommessa);
	}
    public interface IGeCo {
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
        //Iscrizione di uno studente a un determinato corso. Lo puo fare solo lo studente specifico
        void Iscriviti (int idCorso, int idStudente);

        //Cerca un determinato corso 
        Corso SearchCorsi(int idCorso);
        //Cerca tutti i corsi che contine la "descrizione" nei suoi attributi(nome,descrizione)
        List<Corso> SearchCorsi(string descrizione);
        //Cerca tutti i corsi che contiene la "descrizione" di un determinato studente(idStudente)
        List<Corso>SearchCorsi(string descrizione, int idUtente);
        //Mostra tutti i corsi presenti nel db
        List<Corso>ListaCorsi();
        //Mostra tutti i corsi a cui è iscritto un determinato studente(idStudente)
        List<Corso>ListaCorsi(int idUtente);
    }
    public class Studente{ 
        public int Id{get;set;}
        public string Nome{get;set;}
        public string Cognome{get;set;}
    }
    public class Corso{ 
        public int Id{get;set;}
        public string Nome{get;set;}
        public string Descrizione{get;set;}
        public DateTime Inizio{get;set;}
        public DateTime Fine {get;set;}
        public List<Studente> Studenti{get;set;}
        public List<Lezione> Lezioni{get;set;}
    }
    public class Lezione{ 
        public int Id{get;set;}
        public string Nome {get;set;}
        public string Descrizione{get;set;}
        public int Durata{get;set;}
    }
	public partial class Giorno {
		private string _id_utente;
		private DateTime data;

		public DateTime Data { get { return data; } }
		private List<OreCommessa> commesse;
		public string ID_UTENTE { get { return _id_utente; } set { _id_utente = value; } }
		public int HPermesso{ get;set;}
		public int HMalattia{ get;set;}
		public int HFerie{ get;set;}
		public List<OreCommessa> OreLavorate { get => commesse; }


		public Giorno(DateTime data) { this.data = data; }
		public Giorno(DateTime data, int HP, int HM, int HF, string id_utente) {
			this.data = data;
			HPermesso = HP;
			HMalattia = HM;
			HFerie = HF;
			_id_utente = id_utente;
		}

		public void AddOreCommessa(OreCommessa com) {
			if (commesse == null)
				commesse = new List<OreCommessa>();
			commesse.Add(com);
		}
		public int TotOreLavorate() {
			int tot = 0;
			foreach (OreCommessa com in OreLavorate) {
				tot += com.Ore;
			}
			return tot;
		}
		public override bool Equals(object obj) {
			return data.Equals(((Giorno)obj).data);
		}
		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
	public partial class OreCommessa {
		public int Id{ get;set;}
		public string Descrizione { get => _descrizione; set => _descrizione = value; }
		public string Nome { get => _nome; set => _nome = value; }
		public int Ore{ get; set; }

		private int oreLavorate;
		private string _nome;
		private string _descrizione;

		public OreCommessa(int id, int oreLavorate, string nome, string descrizione) {
			Id = id;
			this.oreLavorate = oreLavorate;
			_nome = nome;
			_descrizione = descrizione;
		}
    }
	public class Commessa {
		public int Id { get; set; }
		public string Descrizione { get; set; }
		public string Nome { get; set; }
		public int Capienza { get; set; }
		public int OreLavorate { get; set; }

		public Commessa(int id, string nome, string descrizione, int capienza, int oreLavorate) {
			Id = id;
			OreLavorate = oreLavorate;
			Nome = nome;
			Descrizione = descrizione;
			Capienza = capienza;
		}
	}

	public class CV {
        public string matricola;
        public string nome;
        public string cognome;
        public int eta;
        public string residenza;
        public string telefono;
        public List<EspLav> esperienze;
        public List<PerStud> percorsostudi;
        public List<Competenza> competenze;
    }
    public class EspLav {
        public DateTime AnnoInizio;
        public DateTime AnnoFine;
        public string qualifica;
        public string descrizione;
    }
    public class PerStud {
        public DateTime AnnoInizio;
        public DateTime AnnoFine;
        public string titolo;
        public string descrizione;
    }
    public class Competenza {
        public string titolo;
        public int livello;
    }


}