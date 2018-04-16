using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Gestione.Models{
    partial class DomainModel:IGeCo,IGeCV,IGeTime{
		public List<Corso> ListaCorsi(string idUtente){
			DataAccessObject dto = new DataAccessObject();
			List<Corso> result = dto.FindCorso(idUtente);
			return result;
		}
    }
}