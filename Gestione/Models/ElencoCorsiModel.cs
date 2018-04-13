using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using DAO;

namespace Gestione.Models{
    partial class DomainModel:IGeCo,IGeCV,IGeTime{
		public List<Corso> ListaCorsi(string idUtente){
			DataAccesObject dto = new DataAccesObject();
			List<Corso> result = dto.ListaCorsi(idUtente);
			return result;
		}
		public List<Corso> ListaCorsi(){
			DataAccesObject dto = new DataAccesObject();
			List<Corso> result = dto.ListaCorsi();
			return result;
		}
		public Corso SearchCorsi(int idCorso){
			DataAccesObject dto = new DataAccesObject();
			Corso result = dto.SearchCorsi(idCorso);
			return result;
		}
		public List<Corso> SearchCorsi (string descrizione){
			DataAccesObject dto = new DataAccesObject();
			List<Corso> result = new List<Corso>();
			result = dto.SearchCorsi(descrizione);
			return result;
		}
		public List<Corso> SearchCorsi (string descrizione,string idUtente){
			DataAccesObject dto = new DataAccesObject();
			List<Corso> result = dto.SearchCorsi(descrizione,idUtente);
			return result;
		}
		
    }
}