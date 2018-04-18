﻿using System;
using System.Collections.Generic;
using Interfaces;
using DAO;

namespace Gestione.Models{
	public partial class DomainModel : IGeCo, IGeCV, IGeTime {
		DataAccesObject dao = new DataAccesObject();
		public List<Lezione> ListaLezioni(Corso input){
			return dao.ListaLezioni(input);
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

		public void Iscriviti(int idCorso,string idStudente) {
			try{
				dao.Iscriviti(idCorso,idStudente);
			}catch(Exception e){				
				throw e ;
			}
		}
		public void AddLezione(int idCorso, Lezione lezione){			
			try{
				dao.AddLezione(idCorso,lezione);
			}catch(Exception e){
				throw e;
			}
		}
		public List<Corso> ListaCorsi(string idUtente){
			try{
				return dao.ListaCorsi(idUtente);
			}catch(Exception e){ 
                throw e; 
            }
		}
		public List<Corso> ListaCorsi(){
			try{
				return dao.ListaCorsi();
			}catch(Exception e){ 
                throw e; 
            }
		}
		public Corso SearchCorsi(int idCorso){			
			try{
				return dao.SearchCorsi(idCorso);
			}catch(Exception e){ 
                throw e; 
            }
		}
		public List<Corso> SearchCorsi (string descrizione){
			try{
				return  dao.SearchCorsi(descrizione);
			}catch(Exception e){ 
                throw e; 
            }
		}
		public List<Corso> SearchCorsi (string descrizione,string idUtente){
			try{
				return dao.SearchCorsi(descrizione,idUtente);
			}catch(Exception e){ 
                throw e; 
            }
		}
		public void AddCorso(Corso corso){
            try{ 
               dao.AddCorso(corso);
            }catch(Exception e){ 
                throw e; 
            }
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