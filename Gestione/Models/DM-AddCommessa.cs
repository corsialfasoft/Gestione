using System;
using System.Collections.Generic;
using Interfaces;
using DAO;
using Gestione.Controllers;

namespace Gestione.Models {
	public partial class DomainModel : IGeCo, IGeCV, IGeTime {
		public void AddCommessa(DTCommessa commessa){
			Commessa newCommessa = null;
			newCommessa = new Commessa();
			newCommessa.Nome = commessa.Nome;
			newCommessa.Descrizione = commessa.Descrizione;
			newCommessa.Capienza = commessa.Capienza;
			DataAccesObject dao = new DataAccesObject();
			dao.AddCommessa(newCommessa);
		}
	}
}