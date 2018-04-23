using System;
using System.Collections.Generic;
using Interfaces;
using DAO;
using Gestione.Controllers;

namespace Gestione.Models {
	public partial class DomainModel : IGeCo, IGeCV, IGeTime{
		DataAccesObject dao = new DataAccesObject();
		public List<Lezione> ListaLezioni(Corso input){
			try{
				return dao.ListaLezioni(input);
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

		public void AggiungiCV(CV a){
			try{
				dao.AggiungiCV(a);
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
		public void DelEspLav(EspLav espLav,string matricola)
		{
			dao.DelEspLav(espLav,matricola);
		}

		public void Iscriviti(int idCorso,int idStudente)
		{
			throw new NotImplementedException();
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

		public void EliminaCV(CV curriculum){           
            try{ 
                dao.EliminaCV(curriculum);
            }catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}

        public void ModEspLav(string MatrCv,EspLav espV,EspLav esp){
            try{
				dao.ModEspLav(MatrCv,espV,esp);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }

		public void ModComp(Competenza daMod,Competenza Mod,string matricola){
			try{
				dao.ModComp(matricola,daMod,Mod);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}

		}

		public void ModificaCV(CV c)
		{
			DataAccesObject doo = new DataAccesObject();
            doo.ModificaCV(c);
		}

        public void ModPerStudi(string matricola, PerStud daMod, PerStud Mod){
            try{
				dao.ModPerStudi(matricola,daMod,Mod);
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
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

        public void CaricaCV(string path){
            throw new NotImplementedException();
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

        //GeTime
		public List<DTGiorno> GiorniCommessa(int idCommessa, string idUtente){
			try{ 
				List<Giorno> giorni = dao.GiorniCommessa(idCommessa, idUtente);
				List<DTGiorno> dTGiorni = new List<DTGiorno>();
				if (giorni != null && giorni.Count > 0) {
					foreach (Giorno giorno in giorni) {
						if (giorno.OreLavorate != null && giorno.OreLavorate.Count > 0) 
							dTGiorni.Add(new DTGiorno { Data = giorno.Data, OreLavorate = giorno.OreLavorate[0].Ore });
					}
				}
				return dTGiorni;
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
        }

		public DTCommessa CercaCommessa(string nomeCommessa){
			try{ 
				Commessa commessa = dao.CercaCommessa(nomeCommessa);
				if(commessa!=null){
					return new  DTCommessa(commessa.Id,commessa.Nome,commessa.Descrizione,commessa.Capienza,commessa.OreLavorate);
				}
				return null;
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
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
					DTgiorno.TotOreLavorate = giornoInterface.TotOreLavorate();
					return DTgiorno;
				}
				return null;
			}catch(SystemException){
				throw new Exception("Errore di sistema!");
			}catch(Exception e){
				throw e;
			}
		}

		public void DelCompetenza(Competenza competenza,string matricola)
		{
			dao.DelCompetenza(competenza,matricola);
		}
		public void DelPerStud(PerStud ps,string matricola) {
			DataAccesObject dao = new DataAccesObject();
			dao.DelPerStud(ps,matricola);
		}
	}
}