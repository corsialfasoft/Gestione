using System;
using System.Collections.Generic;
using Gestione.Controllers;
using System.Web;

namespace Gestione.Models {

	public class Profilo {
		private const string PROFILE_VARIABLE = "profile";
		private List<String> Funzioni { get; }
		public string Matricola { get; }
		public string Nome { get; }
		public string Cognome { get; }

		internal Profilo() { }

		internal Profilo(string matricola,List<String> funzioni,string nome,string cognome){
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

		public Profilo GetProfile() {
			//todo if not set load default profile
			throw new NotImplementedException();
		}

		public bool Login(string username,string password) {
			//TODO set profile in session.
			throw new NotImplementedException();
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

		public bool Login(string username,string password) {
			return true;
		}
	}


}