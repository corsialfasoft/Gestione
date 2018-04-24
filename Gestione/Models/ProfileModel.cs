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
		
	}
	public partial class ProfileModel: IProfileModel{
		//private ILoginDao dao;
		private static ProfileModel Profile;
		private HttpSessionStateBase session;
		public Profilo profile {get;}
		public static ProfileModel Instance(HttpSessionStateBase session){
			if(Profile==null) Profile = new ProfileModel();
			Profile.session= session;
			return Profile;
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

		public Profilo GetProfile() {
			List<string> funzioni = new List<string>(){"RicercaCurriculum"};
			return new Profilo("MockMatricola",funzioni,"MockNome","MockCognome");
		}

		public bool Login(string username,string password) {
			return true;
		}
	}


}