using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO;
using Interfaces;

namespace Gestione.Models {
	public partial class DomainModel {
		public List<Giorno> GiorniCommessa(int idCommessa, string idUtente){
			IDao dao = new DataAccesObject();
			return dao.GiorniCommessa(idCommessa,idUtente);
		}
		public Commessa CercaCommessa(string nomeCommessa) {
			IDao dao = new DataAccesObject();
			return dao.CercaCommessa(nomeCommessa);
		}
	}

}