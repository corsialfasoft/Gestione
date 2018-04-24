using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using DAO;
using Gestione.Controllers;
using static Gestione.Controllers.HomeController;

namespace Gestione.Models{
	public partial class DomainModel : IGeCo, IGeCV, IGeTime{
		DataAccesObject dao = new DataAccesObject();
		public List<Lezione> ListaLezioni(Corso input){
			return dao.ListaLezioni(input);
		}

        public void AddCompetenze(string MatrCv,Competenza comp){
            dao.AddCompetenze(MatrCv,comp);
        }

        public void AddCvStudi(string MatrCv,PerStud studi){
            dao.AddCvStudi(MatrCv, studi);
        }

        public void AddEspLav(string MatrCv,EspLav esp){
            dao.AddEspLav(MatrCv,esp);
        }

		public void AggiungiCV(CV a){
			DataAccesObject dao = new DataAccesObject();
            dao.AggiungiCV(a);
		}

		public void Iscriviti(int idCorso,string idStudente){
			try{
				dao.Iscriviti(idCorso,idStudente);
			}catch(Exception e){				
				throw e ;
			}
		}

		public void AddLezione(int idCorso, Lezione lezione){			
			try{
				dao.AddLezione(idCorso,lezione);
			}catch(Exception e){
				throw e;
			}
		}

		public List<Corso> ListaCorsi(string idUtente){
			try{
				return dao.ListaCorsi(idUtente);
			}catch(Exception e){ 
                throw e; 
            }
		}

		public List<Corso> ListaCorsi(){
			try{
				return dao.ListaCorsi();
			}catch(Exception e){ 
                throw e; 
            }
		}

		public Corso SearchCorsi(int idCorso){
			try{
				return dao.SearchCorsi(idCorso);
			}catch(Exception e){ 
                throw e; 
            }
		}

		public List<Corso> SearchCorsi(string descrizione){
			try{
				return  dao.SearchCorsi(descrizione);
			}catch(Exception e){ 
                throw e; 
            }
		}

		public List<Corso> SearchCorsi(string descrizione,string idUtente){
			try{
				return dao.SearchCorsi(descrizione,idUtente);
			}catch(Exception e){ 
                throw e; 
            }
        }

		public void AddCorso(Corso corso){
            try{ 
               dao.AddCorso(corso);
            }catch(Exception e){ 
                throw e; 
            }
        }

		public void ModLezione(Lezione lezione){
			try{
				dao.ModLezione(lezione);
			}catch(Exception e){
				throw e;
			}
		}

		public void EliminaCV(CV curriculum){
            DataAccesObject db = new DataAccesObject();
            try{ 
                db.EliminaCV(curriculum);
            }catch(Exception e){ 
                throw e;    
            }
		}

        public void ModEspLav(string MatrCv,EspLav espV,EspLav esp){
            dao.ModEspLav(MatrCv,espV,esp);
        }

		public void ModComp(Competenza daMod,Competenza Mod,string matricola){
			dao.ModComp(matricola,daMod,Mod);
		}

		public void ModificaCV(string nome,string cognome,int eta,string email,string residenza,string telefono,string matr){
			DataAccesObject doo = new DataAccesObject();
            doo.ModificaCV(nome,cognome,eta,email, residenza,telefono,matr);
		}

        public void ModPerStudi(string matricola, PerStud daMod, PerStud Mod){
            dao.ModPerStudi(matricola,daMod,Mod);
        }

        public CV Search(string id){
			DataAccesObject dao = new DataAccesObject();
			return dao.Search(id);
		}

		public List<CV> SearchChiava(string chiava){
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchChiava(chiava);
		}

		public List<CV> SearchCognome(string cognome){
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchCognome(cognome);
		}

		public List<CV> SearchEta(int eta){
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchEta(eta);
		}

        public void CaricaCV(string path){
            throw new NotImplementedException();
        }

        public List<CV> SearchRange(int etmin,int etmax){
			DataAccesObject dao = new DataAccesObject();
			return dao.SearchRange(etmin,etmax);
		}

        //GeTime
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
			}catch(Exception e){
				throw e;
			}
        }

		public List<DTCommessa> CercaCommessa(string nomeCommessa){
            List<DTCommessa> dtcomm = new List<DTCommessa>();
			try{ 
				List<Commessa> commesse = dao.CercaCommessa(nomeCommessa);
				if(commesse!=null) {
                    foreach (Commessa commessa in commesse) {
                        dtcomm.Add(new DTCommessa(commessa.Id,commessa.Nome,commessa.Descrizione,commessa.Capienza,commessa.OreLavorate));
                    }
                    return dtcomm;
                }
				return null;
			}catch(Exception e){
					throw e;
			}
		}

        public void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente){
            try{
                dao.CompilaHLavoro(data, ore, idCommessa, idUtente);
            }catch(Exception e){
                throw e;
            }
        }

        public void Compila(DateTime data, int ore, HType tipoOre, string idUtente){
            try{
                dao.Compila(data, ore, tipoOre, idUtente);
            }catch(Exception e){
                throw e;
            }
        }

        public DTGGiorno VisualizzaGiorno(DateTime data, string idUtente){
            Giorno giornoInterface = new DataAccesObject().VisualizzaGiorno(data, idUtente);
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
        }
    }
}