using System;
using System.Collections.Generic;
using Interfaces;
using DAO;
using Gestione.Controllers;

namespace Gestione.Models {
	public partial class DomainModel : IGeCo, IGeCV, IGeTime{
		DataAccesObject dao = new DataAccesObject();

        #region GeCo
        public List<Lezione> ListaLezioni(Corso input){
			try{
				return dao.ListaLezioni(input);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public void Iscriviti(int idCorso,string idStudente){
			try{
				dao.Iscriviti(idCorso,idStudente);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public void AddLezione(int idCorso, Lezione lezione){			
			try{
				dao.AddLezione(idCorso,lezione);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public List<Corso> ListaCorsi(string idUtente){
			try{
				return dao.ListaCorsi(idUtente);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public List<Corso> ListaCorsi(){
			try{
				return dao.ListaCorsi();
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public Corso SearchCorsi(int idCorso){
			try{
				return dao.SearchCorsi(idCorso);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione){
			try{
				return  dao.SearchCorsi(descrizione);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione,string idUtente){
			try{
				return dao.SearchCorsi(descrizione,idUtente);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
		public void AddCorso(Corso corso){
            try{ 
               dao.AddCorso(corso);
            }catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
		public void ModLezione(Lezione lezione){
			try{
				dao.ModLezione(lezione);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}

        #endregion
        
        #region GeCv
        public void CaricaCV(string path){
            throw new NotImplementedException();
        }
        
		public void AggiungiCV(CV a){
			try{
				dao.AggiungiCV(a);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
        public void AddCompetenze(string MatrCv,Competenza comp){
            try{
				dao.AddCompetenze(MatrCv,comp);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
        public void AddCvStudi(string MatrCv,PerStud studi){
            try{
				dao.AddCvStudi(MatrCv, studi);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
        public void AddEspLav(string MatrCv,EspLav esp){
            try{
				dao.AddEspLav(MatrCv,esp);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
        
		public void EliminaCV(CV curriculum){           
            try{ 
                dao.EliminaCV(curriculum);
            }catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
        public void DelEspLav(int id) {
			dao.DelEspLav(id);
		}
		public void DelCompetenza(int id) {
			dao.DelCompetenza(id);
		}
		public void DelPerStud(int id) {
			dao.DelPerStud(id);
		}

		public void ModificaCV(CV c ){
			try{
				dao.ModificaCV(c);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
        public void ModEspLav(int id,EspLav esp){
            try{
				dao.ModEspLav(id,esp);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
		public void ModComp(int id,Competenza comp){
			try{
				dao.ModComp(id,comp);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
        public void ModPerStudi(int id, PerStud perStud){
            try{
				dao.ModPerStudi(id,perStud);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }

        public List<EspLav> GetEspLav(string matricola) {
            return dao.GetEspLav(matricola);
        }
        public List<PerStud> GetPerStudi(string matricola) {
            return dao.GetPerStudi(matricola);
        }
        public List<Competenza> GetComp(string matricola) {
            return dao.GetComp(matricola);
        }
        public EspLav GetEsperienza(int id) {
            try{ 
                return dao.GetEsperienza(id);
            }catch(Exception e){ 
                throw e;    
            }
        }
        public PerStud GetPercorso(int id) {
            try{ 
                return dao.GetPercorso(id);
            }catch(Exception e){ 
                throw e;    
            }
        }
        public Competenza GetCompetenza(int id) {
            try{ 
                return dao.GetCompetenza(id);
            }catch(Exception e){ 
                throw e;    
            }
        }

        public CV Search(string id){
			try{
				return dao.Search(id);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public List<CV> SearchChiava(string chiava){
			try{
				return dao.SearchChiava(chiava);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public List<CV> SearchCognome(string cognome){
			try{
				return dao.SearchCognome(cognome);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
		public List<CV> SearchEta(int eta){
			try{
				return dao.SearchEta(eta);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
        public List<CV> SearchRange(int etmin,int etmax){
			try{
				return dao.SearchRange(etmin,etmax);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}

        #endregion
        
        # region GeTime
        public List<DTGiorno> GiorniCommessa(int idCommessa, string idUtente){
			try{ 
				List<Giorno> giorni = dao.GiorniCommessa(idCommessa, idUtente);
				List<DTGiorno> dTGiorni = new List<DTGiorno>();
				if (giorni != null && giorni.Count > 0) {
					foreach (Giorno giorno in giorni) {
						if (giorno.OreLavorate != null && giorno.OreLavorate.Count > 0) {
							dTGiorni.Add(new DTGiorno { Data = giorno.Data, OreLavorate = giorno.OreLavorate[0].Ore });
                        }
					}
				}
				return dTGiorni;
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
		public List<DTCommessa> CercaCommesse(string nomeCommessa){
            List<DTCommessa> dtcomm = new List<DTCommessa>();
			try{ 
				List<Commessa> commesse = dao.CercaCommesse(nomeCommessa);
				if(commesse!=null) {
                    foreach (Commessa commessa in commesse) {
                        dtcomm.Add(new DTCommessa(commessa.Id,commessa.Nome,commessa.Descrizione,commessa.Capienza,commessa.OreLavorate));
                    }
                    return dtcomm;
                }
				return null;
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
        public DTCommessa CercaCommessa(string nomeCommessa) {
            try {
               Commessa commessa = dao.CercaCommessa(nomeCommessa);
                if(commessa!=null)
                    return new DTCommessa(commessa.Id, commessa.Nome, commessa.Descrizione, commessa.Capienza, commessa.OreLavorate);
                return null;
            } catch (SystemException) {
                throw new Exception("Errore di sistema!");
            } catch (Exception e) {
                throw e;
            }
        }
        public void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente){
            try{
                dao.CompilaHLavoro(data, ore, idCommessa, idUtente);
            }catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
        public void Compila(DateTime data, int ore, HType tipoOre, string idUtente){
            try{
                dao.Compila(data, ore, tipoOre, idUtente);
            }catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }
        public DTGGiorno VisualizzaGiorno(DateTime data, string idUtente){
			try{
				Giorno giornoInterface = dao.VisualizzaGiorno(data, idUtente);
				if (giornoInterface != null){
					DTGGiorno DTgiorno = new DTGGiorno{
						data = giornoInterface.Data,
						OrePermesso = giornoInterface.HPermesso,
						OreMalattia = giornoInterface.HMalattia,
						OreFerie = giornoInterface.HFerie
					};
					foreach (OreLavorative orecommessa in giornoInterface.OreLavorate){
						OreLavorate orelavorate = new OreLavorate {
							nome = orecommessa.Nome,
							oreGiorno = orecommessa.Ore,
							descrizione = orecommessa.Descrizione
						};
						DTgiorno.OreLavorate.Add(orelavorate);
					}
					DTgiorno.TotOreLavorate = giornoInterface.TotOreLavorate;
					return DTgiorno;
				}
				return null;
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}
        public List<DTGiornoDMese> DettaglioMese(int anno, int mese, string idutente) {
            try {
                return dao.DettaglioMese(anno, mese, idutente);
            } catch (Exception e) {
                throw e;
            }
        }
        public List<int> Years(string idUtente) {
            try {
                return dao.Years(idUtente);
            } catch (SystemException) {
                throw new Exception("Errore di sistema!");
            } catch (Exception e) {
                throw e;
            }
        }
        public List<int> Month(int year, string idUtente) {
            try {
                return dao.Month(year,idUtente);
            } catch (SystemException) {
                throw new Exception("Errore di sistema!");
            } catch (Exception e) {
                throw e;
            }
        }
        public void AddCommessa(DTCommessa commessa) {
            Commessa newCommessa = null;
            newCommessa = new Commessa();
            newCommessa.Nome = commessa.Nome;
            newCommessa.Descrizione = commessa.Descrizione;
            newCommessa.Capienza = commessa.Capienza;
            DataAccesObject dao = new DataAccesObject();
            dao.AddCommessa(newCommessa);
        }
        #endregion
    }
}