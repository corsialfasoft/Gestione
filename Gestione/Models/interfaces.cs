using System;
using System.Collections.Generic;
using Gestione.Controllers;
using Gestione.Models;
using static Gestione.Controllers.HomeController;

namespace Interfaces {
    public interface IGeCV {
        void ModificaCV(CV c); //modifica un curriculum nel db
        void AggiungiCV(CV a); //quando sei loggato, puoi aggiungere un curriculum nel db
        void CaricaCV(string path); //quando non sei loggato, puoi spedire un curriuculum
        CV Search(string id); //search di un curriculum per id di un curriculum
        List<CV> SearchChiava(string chiava); //search generale per parole chiava 
        List<CV> SearchEta(int eta); //search solo per quella precisa età
        List<CV> SearchRange(int etmin, int etmax); //search per un range di età minimo e massimo
        void EliminaCV(CV curriculum); //Elimina un CV dal db
        List<CV> SearchCognome(string cognome); //Ricerca solo per cognome
        void AddCvStudi(string MatrCv, PerStud studi);
        void AddEspLav(string MatrCv, EspLav esp);
        void AddCompetenze(string MatrCv, Competenza comp);
		void DelEspLav(EspLav espLav , string matricola);
		void DelCompetenza(Competenza comp , string matricola);
		void DelPerStud(PerStud ps , string matricola);
        void ModEspLav(string MatrCv, EspLav espV, EspLav esp );	
		void ModComp(Competenza daMod , Competenza Mod , string matricola); // Modifica la singola competenza
        void ModPerStudi(string matricola, PerStud daMod, PerStud Mod);
    }
	public enum HType { HMalattia = 1, HPermesso, HFerie }
	interface IGeTime {
		void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, string idUtente);
		DTGGiorno VisualizzaGiorno(DateTime data, string idUtente);
		List<DTGiorno> GiorniCommessa(int idCommessa, string idUtente);
		List<DTCommessa> CercaCommesse(string nomeCommessa);
        DTCommessa CercaCommessa(string nomeCommessa);
        List<int> Years(string idUtente);
        List<int> Month(int year, string idUtente);
		void AddCommessa(DTCommessa commessa);
	}
    public interface IGeCo {
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
        //Iscrizione di uno studente a un determinato corso. Lo puo fare solo lo studente specifico
        void Iscriviti(int idCorso, string idStudente);
        //Cerca un determinato corso 
        Corso SearchCorsi(int idCorso);
        //Cerca tutti i corsi che contine la "descrizione" nei suoi attributi(nome,descrizione)
        List<Corso> SearchCorsi(string descrizione);
        //Cerca tutti i corsi che contiene la "descrizione" di un determinato studente(idStudente)
        List<Corso> SearchCorsi(string descrizione, string idUtente);
        //Mostra tutti i corsi presenti nel db
        List<Corso> ListaCorsi();
        //Mostra tutti i corsi a cui è iscritto un determinato studente(idStudente)
        List<Corso> ListaCorsi(string idUtente);
        //Mostra la lista delle lezioni relative a un corso
        List<Lezione> ListaLezioni(Corso corso);
       
		void EliminaLezione(int Id);
		//Elimina una lezione
        //Modifica Lezione
		void ModLezione(Lezione lezione);
		//Modifica Corso
		void ModificaCorso(int IdCorsoToMod, Corso NuovoCorso);

    }
    public class Studente {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
    public class Corso {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public DateTime Inizio { get; set; }
        public DateTime Fine { get; set; }
        public List<Studente> Studenti { get; set; }
        public List<Lezione> Lezioni { get; set; }
        public Corso() { }
        public Corso(int id, string descrizione, List<Lezione> leziones) {
            this.Id = id;
            this.Descrizione = descrizione;
            this.Lezioni = leziones;
        }
    }
    public class Lezione {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public int Durata { get; set; }
        public Lezione() { }
        public Lezione(string nome) {
            this.Nome = nome;
        }
    }
    public partial class Giorno {
        private string _id_utente;
        private DateTime data;
        private int idG;
        public DateTime Data { get { return data; } set { data = value; } }
        private List<OreLavorative> oreLavorative = new List<OreLavorative>();
        public string ID_UTENTE { get { return _id_utente; } set { _id_utente = value; } }
        public int HPermesso { get; set; }
        public int HMalattia { get; set; }
        public int HFerie { get; set; }
        public List<OreLavorative> OreLavorate { get => oreLavorative; }
        public int IdGiorno { get; set; }
        public int TotOreLavorate { get; set; }

        public Giorno(DateTime data) { this.data = data; }
        public Giorno(DateTime data, int idG, int HP, int HM, int HF, int oreLavorate, string id_utente):this(data,idG,HP,HM,HF,id_utente) {
            TotOreLavorate = oreLavorate;
        }
        public Giorno(DateTime data, int idG, int HP, int HM, int HF, string id_utente) {
            this.data = data;
            HPermesso = HP;
            HMalattia = HM;
            HFerie = HF;
            _id_utente = id_utente;
            this.idG = idG;
        }

        public void AddOreLavorative(OreLavorative com) {
            oreLavorative.Add(com);
            this.TotOreLavorate += com.Ore;
        }

        public override bool Equals(object obj) {
            return data.Equals(((Giorno)obj).data);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
    public partial class OreLavorative {
        public int IdC { get; set; }
        public string Descrizione { get => _descrizione; set => _descrizione = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public int Ore { get; set; }
        private string _nome;
        private string _descrizione;

        public OreLavorative(int idC, int oreLavorate, string nome, string descrizione) {
            this.IdC = idC;
            Ore = oreLavorate;
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

		public Commessa(){ }

        public Commessa(int id, string nome, string descrizione, int capienza, int oreLavorate) {
            Id = id;
            OreLavorate = oreLavorate;
            Nome = nome;
            Descrizione = descrizione;
            Capienza = capienza;
        }
    }
    public class CV {
        public string Matricola { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Eta { get; set; }
        public string Email { get; set; }
        public string Residenza { get; set; }
        public string Telefono { get; set; }
        public List<EspLav> Esperienze { get; set; }
        public List<PerStud> Percorsostudi { get; set; }
        public List<Competenza> Competenze { get; set; }
        public CV() {
            Esperienze = new List<EspLav>();
            Percorsostudi = new List<PerStud>();
            Competenze = new List<Competenza>();
        }
    }
    public class EspLav {
        public int AnnoInizio { get; set; }
        public int AnnoFine { get; set; }
        public string Qualifica { get; set; }
        public string Descrizione { get; set; }
    }
    public class PerStud {
        public int AnnoInizio { get; set; }
        public int AnnoFine { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
    }
    public class Competenza {
        public string Titolo { get; set; }
        public int Livello { get; set; }
    }
}