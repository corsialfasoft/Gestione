using System;
using System.Collections.Generic;
using Interfaces;
using DAO;

namespace Gestione.Models{
	public partial class DomainModel : IGeCo, IGeCV, IGeTime
	{
		public void AddCorso(Corso corso)
		{
			throw new NotImplementedException();
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

		public void ModificaCV(CV a,CV b)
		{
			DataAccesObject doo = new DataAccesObject();
            doo.ModificaCV(a,b);
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


        public string SearchMatricola(string usr,string psw) {
            DataAccesObject dao = new DataAccesObject();
            try{
                string matricola = dao.SearchMatricola(usr,psw);
                if(matricola != "") {
                    return matricola;
                } else {
                    throw new Exception();
                }
            } catch(Exception e) {
                throw e;
            }
        }

        public void IscrizioneAlPortale(string nome, string cognome,string usr,string pass) {
            DataAccesObject dao = new DataAccesObject();
            try{
                dao.IscrizioneAlPortale(nome,cognome,usr,pass);
            } catch(Exception e) {
                throw e;
            }
        }

        public Profilo SearchProfile(string usr,string pass) {
            DataAccesObject dao = new DataAccesObject();
            Profilo prf = new Profilo();
            prf = dao.SearchProfile(SearchMatricola(usr,pass));
            return prf;
        }
	}
}