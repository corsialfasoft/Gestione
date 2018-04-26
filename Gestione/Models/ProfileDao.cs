using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Interfaces;
using LibreriaDB;
using System.Data;
using Gestione.Models;
using DAO;

namespace ProfileDao{
	public interface ProfilaturaDao{
		bool Login(string username, string password);
		Profilo GetProfile();
		void IscrizioneAlPortale(string nome,string cognome, string usr,string psw);
	}
	public class ProfiloDao : ProfilaturaDao{
		ITrasformer transf = new Trasformator();
		public Profilo GetProfile(string username,string password){
			try{
				SqlParameter[] param = {new SqlParameter("@usr", username),
                    new SqlParameter("@pass",password)};
				Profilo output = DB.ExecQProcedureReader("GetProfile",transf.TransfInProfilo,param, "Profilatura");
				SqlParameter[] paramss = {new SqlParameter("@usr", username),
                    new SqlParameter("@pass",password)};
                List<string> funzioni =  DB.ExecQProcedureReader("GetFunzioni",transf.TransfInFunzioni,paramss, "Profilatura");
                output.Funzioni = funzioni;
				return output;
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
		public void IscrizioneAlPortale(string nome,string cognome, string usr,string psw){
			try{
				SqlParameter[] param = {new SqlParameter("@nome", nome),
										new SqlParameter("@cognome", cognome),
										new SqlParameter("@usr", usr),
										new SqlParameter("@psw", psw)
										};
				DB.ExecNonQProcedure("IscrizioneAlPortale",param,"Profilatura");
			} catch(SqlException){
				throw new Exception("Errore server!");
			} catch(Exception e){
				throw e;
			}
		}
	}
}