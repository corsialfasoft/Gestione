﻿using System;
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
        List<Corso>SearchCorsi(string descrizione, string idUtente);
        //Mostra tutti i corsi presenti nel db
        List<Corso>ListaCorsi();
        //Mostra tutti i corsi a cui è iscritto un determinato studente(idStudente)
        List<Corso>ListaCorsi(string idUtente);
    }
	public partial class DataAccesObject : IDao {
		public void AddCorso(Corso corso) {
			throw new NotImplementedException();
		}

		public void AddLezione(int idCorso,Lezione lezione) {
			throw new NotImplementedException();
		}

		public void AggiungiCV(CV a) {
			throw new NotImplementedException();
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
			Corso c = new Corso();
			c.Nome = "c#";
			c.Descrizione= "Corso di programmazione su Asp.Net";
			c.Id = 1 ;
			Corso d = new Corso();
			d.Nome= "Java";
			d.Descrizione= "Corso alla proggrammazione OO";
			d.Id=2;
			Corso e = new Corso();
			e.Nome = "Javascripppto";
			e.Descrizione ="Corso alla programazione su javascripttto";
			e.Id = 3;
			List<Corso> result = new List<Corso>();
			result.Add(c);
			result.Add(d);
			result.Add(e);
			return result;
		}

		public List<Corso> ListaCorsi(string idUtente) {
			Corso c = new Corso();
			c.Nome = "c#";
			c.Descrizione= "Corso di cerca idutente programmazione su Asp.Net";
			c.Id = 1 ;
			Corso d = new Corso();
			d.Nome= "Java";
			d.Descrizione= "Corso alla c proggrammazione OO cerca idutente";
			d.Id=2;
			Corso e = new Corso();
			e.Nome = "Javascripppto";
			e.Descrizione ="Corso alla programazione su javascripttto cerca idutente";
			e.Id = 3;
			List<Corso> result = new List<Corso>();
			result.Add(c);
			result.Add(d);
			result.Add(e);
			return result;
		}

		public void ModificaCV(CV a,CV b) {
			throw new NotImplementedException();
		}

		public CV Search(string id) {
			throw new NotImplementedException();
		}

		public List<CV> SearchChiava(string chiava) {
			throw new NotImplementedException();
		}

		public List<CV> SearchCognome(string cognome) {
			throw new NotImplementedException();
		}

		public Corso SearchCorsi(int idCorso) {
			Corso c = new Corso();
			c.Id=4;
			c.Nome="C";
			c.Descrizione= "Search corso per idCorso!";
			return c;
		}

		public List<Corso> SearchCorsi(string descrizione) {
				Corso c = new Corso();
			c.Nome = "c#";
			c.Descrizione= "Corso di cerca idutente programmazione su Asp.Net descrizione";
			c.Id = 1 ;
			Corso d = new Corso();
			d.Nome= "Java";
			d.Descrizione= "Corso alla c proggrammazione OO  descrizione";
			d.Id=2;
			Corso e = new Corso();
			e.Nome = "Javascripppto";
			e.Descrizione ="Corso alla programazione su javascripttto descrizione";
			e.Id = 3;
			List<Corso> result = new List<Corso>();
			result.Add(c);
			result.Add(d);
			result.Add(e);
			return result;
		}

		public List<Corso> SearchCorsi(string descrizione,string idUtente) {
				Corso c = new Corso();
			c.Nome = "c#";
			c.Descrizione= "Corso di cerca idutente programmazione su Asp.Net";
			c.Id = 1 ;
			Corso d = new Corso();
			d.Nome= "Java";
			d.Descrizione= "Corso alla c proggrammazione OO descrizione  cerca idutente";
			d.Id=2;
			Corso e = new Corso();
			e.Nome = "Javascripppto";
			e.Descrizione ="Corso alla programazione su javascripttto descrizione cerca idutente";
			e.Id = 3;
			List<Corso> result = new List<Corso>();
			result.Add(c);
			result.Add(d);
			result.Add(e);
			return result;
		}

		public List<CV> SearchEta(int eta) {
			throw new NotImplementedException();
		}

		public List<CV> SearchRange(int etmin,int etmax) {
			throw new NotImplementedException();
		}

		public Giorno VisualizzaGiorno(DateTime data,int idUtente) {
			throw new NotImplementedException();
		}
	}
}