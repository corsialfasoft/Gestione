using System;
using System.Collections.Generic;
using Interfaces;
using DAO;

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
			DataAccesObject dao = new DataAccesObject();
			try{
			dao.Iscriviti(idCorso,idStudente);
			}catch(Exception e){				
				throw e ;
			}
		}

		public Interfaces.Corso SearchCorsi(int idCorso) {
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchCorsi(idCorso);
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


		public List<Interfaces.Corso> SearchCorsi(string descrizione) {
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchCorsi(descrizione);
		}

		public List<Interfaces.Corso> SearchCorsi(string descrizione, string idUtente) {
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchCorsi(descrizione, idUtente);
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