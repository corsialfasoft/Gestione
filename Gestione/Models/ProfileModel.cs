using System;
using System.Collections.Generic;
using Gestione.Controllers;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using LibreriaDB;
using DAO;

namespace Gestione.Models {

	public class Profilo {
		private const string PROFILE_VARIABLE = "profile";
		public List<string> Funzioni { get; set;}
		public string Matricola { get; set; }
		public string Nome { get; set;}
		public string Cognome { get; set;}

		internal Profilo() { }

		internal Profilo(string matricola,List<string> funzioni,string nome,string cognome){
			Matricola = matricola;
			Funzioni = funzioni;
			Nome = nome;
			Cognome = cognome;
		}
		public bool CheckFunction(string functionName) {
			return Funzioni.Contains(functionName);
		}
	}

	
	public interface IProfileModel {
		bool Login(string username, string password);
		Profilo GetProfile();
		void IscrizioneAlPortale(string nome,string cognome, string usr,string psw);
		
	}
	public partial class ProfileModel: IProfileModel{
        ITrasformer transf = new Trasformator();
		private static ProfileModel Profile;
		private HttpSessionStateBase session;
		public Profilo profile {get;}
		public static ProfileModel Instance(HttpSessionStateBase session){
			if(Profile==null) Profile = new ProfileModel();
			Profile.session= session;
			return Profile;
		}
		public void IscrizioneAlPortale(string nome,string cognome, string usr,string psw){
			try{
				dao.IscrizioneAlPortale(nome, cognome, usr,psw);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}

		//public bool Login(string username,string password) {
		//	//TODO set profile in session.
		//	throw new NotImplementedException();
		//}

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
	}

	public class ProfileMock : IProfileModel {
		internal static ProfileMock Instance(HttpSessionStateBase session) {
			return new ProfileMock();
		}
		public void IscrizioneAlPortale(string nome,string cognome, string usr,string psw){
			try{
				dao.IscrizioneAlPortale(nome, cognome, usr, psw);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public Profilo GetProfile() {
			List<string> funzioni = new List<string>(){"RicercaCurriculum"};
			return new Profilo("MkMatric",funzioni,"MkNome","MkCognome");
		}

	//	public bool Login(string username,string password) {
	//		return true;
	//	}
	//}


}