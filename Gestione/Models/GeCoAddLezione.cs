using DAO;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gestione.Models {
    public partial class DomainModel{
		public void AddLezione(int idCorso, Lezione lezione){
			DataAccesObject dao = new DataAccesObject();
			try{
				dao.AddLezione(idCorso,lezione);
			}catch(Exception e){
				throw e;
			}
		}
    }
}