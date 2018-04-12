using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Gestione.Models{
    partial class DomainModel:IGeCo,IGeCV,IGeTime{
		public List<Corso> ListaCorsi(int idUtente){
			DataAccessObject dto = new DataAccessObject();
			List<Corso> result = dto.FindCorsoUtente(idUtente);
			return result;
		}
		public List<Corso> ListaCorsi(){
			DataAccessObject dto = new DataAccessObject();
			List<Corso> result = dto.FindCorsi();
			return result;
		}
		public Corso SearchCorsi(int idCorso){
			DataAccessObject dto = new DataAccessObject();
			Corso result = dto.FindCorsoID(idCorso);
			return result;
		}
		public List<Corso> SearchCorsi (string descrizione){
			DataAccessObject dto = new DataAccessObject();
			List<Corso> result = dto.FindCorsiDescr(descrizione);
			return result;
		}
		public List<Corso> SearchCorsi (string descrizione,int idUtente){
			DataAccessObject dto = new DataAccessObject();
			List<Corso> result = dto.FindCorsiDescrAndId(descrizione,id);
			return result;
		}
		
    }
}