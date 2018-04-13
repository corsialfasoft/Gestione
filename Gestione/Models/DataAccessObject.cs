using System;
using System.Collections.Generic;
using Interfaces;

namespace DAO{
	public interface IDao{
		void ModificaCV(CV a, CV b); //modifica un curriculum nel db
		void AggiungiCV(CV a); //quando sei loggato, puoi aggiungere un curriculum nel db
		void CaricaCV(string path); //quando non sei loggato, puoi spedire un curriuculum
		CV Search(string id); //search di un curriculum per id di un curriculum
		List<CV> SearchChiava(string chiava); //search generale per parole chiava 
		List<CV> SearchEta(int eta); //search solo per quella precisa età
		List<CV> SearchRange(int etmin, int etmax); //search per un range di età minimo e massimo
		void EliminaCV(CV curriculum); //Elimina un CV dal db
		List<CV> SearchCognome(string cognome); //Ricerca solo per cognome
	
	
	
		void CompilaHLavoro(DateTime data, int ore, int idCommessa, int idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, int idUtente);
		Giorno VisualizzaGiorno(DateTime data, int idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, int idUtente);
		Commessa CercaCommessa(string nomeCommessa);
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
	public partial class DataAccesObject : IDao {
		public void AddCorso(Corso corso) {
			throw new NotImplementedException();
		}

		public void AddLezione(int idCorso,Lezione lezione) {
			throw new NotImplementedException();
		}

		public void AggiungiCV(CV a) {
			//
		}

		public void CaricaCV(string path) {
			throw new NotImplementedException();
		}

		public Commessa CercaCommessa(string nomeCommessa) {
			throw new NotImplementedException();
		}

		public void Compila(DateTime data,int ore,HType tipoOre,int idUtente) {
			throw new NotImplementedException();
		}

		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,int idUtente) {
			throw new NotImplementedException();
		}

		public void EliminaCV(CV curriculum) {
			throw new NotImplementedException();
		}

		public List<Giorno> GiorniCommessa(int idCommessa,int idUtente) {
			throw new NotImplementedException();
		}

		public void Iscriviti(int idCorso,int idStudente) {
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi() {
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi(int idUtente) {
			throw new NotImplementedException();
		}

		public void ModificaCV(CV a,CV b) {
			throw new NotImplementedException();
		}

		public CV Search(string id) {
			return new CV {nome="Massimo",cognome="franzoso",telefono="3391627441",eta=33};
		}

		public List<CV> SearchChiava(string chiava) {
			List<CV> trovati = new List<CV>();
			if (chiava == "truzzotunztunz"){
			CV a = new CV { nome="Pino",cognome="Panino",telefono="123",email="truzzotunztunz"};
			CV b = new CV { nome ="Alex",cognome="dimitri",email="truzzotunztunz"};
			CV c = new CV { nome="Dino",cognome="sauro",email="truzzotunztunz"};
			trovati.Add(a);
			trovati.Add(b);
			trovati.Add(c);
			}
			return trovati;
		}

		public List<CV> SearchCognome(string cognome) {
			throw new NotImplementedException();
		}

		public Corso SearchCorsi(int idCorso) {
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione) {
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione,int idUtente) {
			throw new NotImplementedException();
		}

		public List<CV> SearchEta(int eta) {
			List<CV> trovati = new List<CV>();
			if (eta == 22){
			CV a = new CV { nome="Pino",cognome="Panino",telefono="123",email="truzzotunztunz",eta=22};
			CV b = new CV { nome ="Alex",cognome="dimitri",email="weasd",eta=22};
			CV c = new CV { nome="Dino",cognome="sauro",email="eeeeee",eta=22};
			trovati.Add(a);
			trovati.Add(b);
			trovati.Add(c);
			}
			return trovati;
		}

		public List<CV> SearchRange(int etmin,int etmax) {
			List<CV> trovati = new List<CV>();
			if (etmin>=22 && etmax<=25){
			CV a = new CV { nome="Pino",cognome="Panino",telefono="123",email="truzzotunztunz",eta=25};
			CV b = new CV { nome ="Alex",cognome="dimitri",email="weasd",eta=22};
			
			trovati.Add(a);
			trovati.Add(b);
			trovati.Add(c);
			}
			return trovati;
		}

		public Giorno VisualizzaGiorno(DateTime data,int idUtente) {
			throw new NotImplementedException();
		}
	}
}