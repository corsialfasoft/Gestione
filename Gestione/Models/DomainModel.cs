using System;
using System.Collections.Generic;
using Interfaces;
using DAO;

namespace Gestione.Models{
	public partial class DomainModel : IGeCo, IGeCV, IGeTime{
	DataAccesObject dao = new DataAccesObject();	
        public void AddCompetenze(string MatrCv,Competenza comp) {
            dao.AddCompetenze(MatrCv,comp);
        }

        public void AddCorso(Corso corso)
		{
			throw new NotImplementedException();
		}

        public void AddCvStudi(string MatrCv,PerStud studi) {
            dao.AddCvStudi(MatrCv, studi);
        }

        public void AddEspLav(string MatrCv,EspLav esp) {
            dao.AddEspLav(MatrCv,esp);
        }

        public void AddLezione(int idCorso,Lezione lezione)
		{
			throw new NotImplementedException();
		}

		public void AggiungiCV(CV a)
		{
			DataAccesObject doo = new DataAccesObject();
            doo.AggiungiCV(a);
		}

		public void CaricaCV(string path)
		{
			throw new NotImplementedException();
		}

		public Commessa CercaCommessa(string nomeCommessa)
		{
			throw new NotImplementedException();
		}

		public void Compila(DateTime data,int ore,HType tipoOre,int idUtente)
		{
			throw new NotImplementedException();
		}

		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,int idUtente)
		{
			throw new NotImplementedException();
		}

		public void EliminaCV(CV curriculum)
		{
            DataAccesObject db = new DataAccesObject();
            try{ 
                db.EliminaCV(curriculum);
            }catch(Exception e){ 
                throw e;    
            }
		}

		public List<Giorno> GiorniCommessa(int idCommessa,int idUtente)
		{
			throw new NotImplementedException();
		}

		public void Iscriviti(int idCorso,int idStudente)
		{
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi()
		{
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi(int idUtente)
		{
			throw new NotImplementedException();
		}

        public void ModEspLav(string MatrCv,EspLav espV,EspLav esp) {
            dao.ModEspLav(MatrCv,espV,esp);
        }

        public void ModificaCV(CV a,CV b) ///////////////////////////
		public void ModComp(Competenza daMod,Competenza Mod,string matricola) {
			dao.ModComp(matricola,daMod,Mod);

		}

		public void ModificaCV(CV a,CV b)
		{
			DataAccesObject doo = new DataAccesObject();
            doo.ModificaCV(a,b);
		}

        public void ModPerStudi(string matricola, PerStud daMod, PerStud Mod) {
            dao.ModPerStudi(matricola,daMod,Mod);
        }

        public CV Search(string id)
		{
			DataAccesObject dao = new DataAccesObject();
			return dao.Search(id);
		}

		public List<CV> SearchChiava(string chiava)
		{
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchChiava(chiava);
		}

		public List<CV> SearchCognome(string cognome)
		{
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchCognome(cognome);
		}

		public Corso SearchCorsi(int idCorso)
		{
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione)
		{
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione,int idUtente)
		{
			throw new NotImplementedException();
		}

		public List<CV> SearchEta(int eta)
		{
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchEta(eta);
		}

		public List<CV> SearchRange(int etmin,int etmax)
		{
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchRange(etmin,etmax);
		}

		public Giorno VisualizzaGiorno(DateTime data,int idUtente)
		{
			throw new NotImplementedException();
		}
	}
}