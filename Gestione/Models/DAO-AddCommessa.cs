using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Interfaces;
using LibreriaDB;
using System.Data;
using Gestione.Controllers;

namespace DAO {
	public partial class DataAccesObject : IDao {
		public void AddCommessa(Commessa commessa) {
			try {
				SqlParameter[] param = {
					new SqlParameter ("@nome", commessa.Nome),
					new SqlParameter("@descrizione", commessa.Descrizione),
					new SqlParameter("@capienza", commessa.Capienza)
				};
				int RowAffected = DB.ExecNonQProcedure("SP_AddCommessa", param, "GeTime");
				if (RowAffected == 0) {
					throw new Exception("Commessa non aggiunta");
				}
			} catch (SqlException) {
				throw new Exception("Errore server!");
			} catch (Exception e) {
				throw e;
			}
		}		
	}
}
