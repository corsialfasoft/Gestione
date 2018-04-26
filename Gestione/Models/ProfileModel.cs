using System;
using System.Collections.Generic;
using System.Web;
using DAO;
using ProfileDao;

namespace Gestione.Models {
	public class Profilo {
		private const string PROFILE_VARIABLE = "profile";
		public List<string> Funzioni { get; set;}
		public string Matricola { get; set; }
		public string Nome { get; set;}
		public string Cognome { get; set;}

		internal Profilo() { }

		//internal Profilo(string matricola,List<string> funzioni,string nome,string cognome){
		//	Matricola = matricola;
		//	Funzioni = funzioni;
		//	Nome = nome;
		//	Cognome = cognome;
		//}
		public bool CheckFunction(string functionName) {
			return Funzioni.Contains(functionName);
		}
	}
	
	public interface IProfileModel {
		Profilo GetProfile(string username,string password);
		void IscrizioneAlPortale(string nome,string cognome, string usr,string psw);		
	}

	public partial class ProfileModel: IProfileModel{        
		IProfilaturaDao profdao = new ProfiloDao();
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
				profdao.IscrizioneAlPortale(nome, cognome, usr,psw);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public Profilo GetProfile(string username,string password){
			try{
				return profdao.GetProfile(username,password);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
	}
}