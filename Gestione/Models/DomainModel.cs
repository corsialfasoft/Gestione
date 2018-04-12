using System;
using System.Collections.Generic;
using Interfaces;

namespace Gestione.Models{
	public partial class DomainModel : IGeCo, IGeCV, IGeTime {
		public void AddCorso(Interfaces.Corso corso) {
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
			
		}

		public List<Interfaces.Corso> ListaCorsi() {
			throw new NotImplementedException();
		}

		public List<Interfaces.Corso> ListaCorsi(int idUtente) {
			throw new NotImplementedException();
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

		public Interfaces.Corso SearchCorsi(int idCorso) {
			throw new NotImplementedException();
		}

		public List<Interfaces.Corso> SearchCorsi(string descrizione) {
			throw new NotImplementedException();
		}

		public List<Interfaces.Corso> SearchCorsi(string descrizione,int idUtente) {
			throw new NotImplementedException();
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