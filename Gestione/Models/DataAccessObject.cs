using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Interfaces;
using LibreriaDB;

namespace DAO {
	public interface IDao{
		void ModificaCV(string nome,string cognome,int eta,string email,string residenza,string telefono,string matr); //modifica un curriculum nel db
		void AggiungiCV(CV a); //quando sei loggato, puoi aggiungere un curriculum nel db
		void CaricaCV(string path); //quando non sei loggato, puoi spedire un curriuculum
		CV Search(string id); //search di un curriculum per id di un curriculum
		List<CV> SearchChiava(string chiava); //search generale per parole chiava 
		List<CV> SearchEta(int eta); //search solo per quella precisa età
		List<CV> SearchRange(int etmin, int etmax); //search per un range di età minimo e massimo
		void EliminaCV(CV curriculum); //Elimina un CV dal db
		List<CV> SearchCognome(string cognome); //Ricerca solo per cognome
        void AddCvStudi(string MatrCv,PerStud studi);
        void AddEspLav(string MatrCv, EspLav esp );
        void AddCompetenze(string MatrCv, Competenza comp);
        void ModEspLav(string MatrCv, EspLav espV, EspLav esp );
		void ModComp( string matricola, Competenza daMod , Competenza Mod ); // Modifica la singola competenza
	
        void ModPerStudi(string matricola, PerStud daMod, PerStud Mod);


        void CompilaHLavoro(DateTime data, int ore, int idCommessa, string idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, string idUtente);
		Giorno VisualizzaGiorno(DateTime data, string idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, string idUtente);
		Commessa CercaCommessa(string nomeCommessa);
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
		void ModLezione(Lezione lezione);
        //Iscrizione di uno studente a un determinato corso. Lo puo fare solo lo studente specifico
        void Iscriviti (int idCorso, string idStudente);

        //Cerca un determinato corso 
        Corso SearchCorsi(int idCorso);
        //Cerca tutti i corsi che contine la "descrizione" nei suoi attributi(nome,descrizione)
        List<Corso> SearchCorsi(string descrizione);
        //Cerca tutti i corsi che contiene la "descrizione" di un determinato studente(idStudente)
        List<Corso>SearchCorsi(string descrizione, string idUtente);
        //Mostra tutti i corsi presenti nel db
        List<Corso>ListaCorsi();
        //Mostra tutti i corsi a cui è iscritto un determinato studente(idStudente)
        List<Corso>ListaCorsi(string idUtente);
		//mostra tutte le lezioni associate a un corso
		List<Lezione> ListaLezioni(Corso corso);
    }
	
	public partial class DataAccesObject : IDao {
		ITrasformer transf = new Trasformator();
        public List<Lezione> ListaLezioni(Corso corso){
			SqlParameter[] param = {new SqlParameter("@IdCorso",corso.Id)};
			return DB.ExecQProcedureReader("ListaLezioni",transf.TrasformInLezioni, param,"GeCorsi");
		}
		public void AddCompetenze(string MatrCv,Competenza comp) {
			try{
				SqlParameter[] param = {
					new SqlParameter("@Tipo",comp.Titolo),
					new SqlParameter("@Livello",comp.Livello),
					new SqlParameter("@MatrCv",MatrCv)					
				};
				int output = DB.ExecNonQProcedure("dbo.AddCompetenze", param,"GeCV");
				if(output == 0){
					throw new Exception("Nessuna competenza aggiunta!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
        public void AddCorso(Corso corso) {
			try{
				SqlParameter[] param = {
					new SqlParameter("@nome", corso.Nome),
					new SqlParameter("@descrizione", corso.Descrizione),
					new SqlParameter("@dInizio", corso.Inizio),
					new SqlParameter("@dFine", corso.Fine)
				};
				int RowAffected = DB.ExecNonQProcedure("AddCorso", param,"GeCorsi");
				if(RowAffected == 0){
					throw new CorsoNonAggiuntaException("Corso non aggiunto") ;
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void AddLezione(int idCorso,Lezione lezione) {
			try{
				SqlParameter[] param = {
					new SqlParameter ("@idCorsi", idCorso),
					new SqlParameter ("@nome", lezione.Nome),
					new SqlParameter("@descrizione", lezione.Descrizione),
					new SqlParameter("@durata", lezione.Durata)
				};
				int RowAffected = DB.ExecNonQProcedure("AddLezione", param,"GeCorsi");
				if(RowAffected == 0){
					throw new LezioneNonAggiuntaException("Lezione non aggiunta") ;
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void ModLezione(Lezione lezione){
			try{
				SqlParameter[] param = {
					new SqlParameter("@idLezione",lezione.Id),
					new SqlParameter("@nome",lezione.Nome),
					new SqlParameter("@descrizione",lezione.Descrizione),
					new SqlParameter("@durata",lezione.Durata)
				};
				int RowAffected =DB.ExecNonQProcedure("ModLezione",param,"GeCorsi",@"(localdb)\MSSQLLocalDB");
				if (RowAffected == 0) {
					throw new LezionNonModificataException("Non hai modificato la lezione");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
        public void AddCvStudi(string MatrCv,PerStud studi) {
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@AnnoI",studi.AnnoInizio),
					new SqlParameter("@AnnoF",studi.AnnoFine),
					new SqlParameter("@Titolo",studi.Titolo),
					new SqlParameter("@Descrizione",studi.Descrizione),
					new SqlParameter("@MatrCv",MatrCv)
				};
				int output = DB.ExecNonQProcedure("dbo.AddCvStudi", parameters, "GeCv");
				if (output == 0) { 
					throw new Exception("Nessun curriculum eliminato!");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
        public void AddEspLav(string MatrCv,EspLav esp) {
			try{
				SqlParameter[] param = { 
					new SqlParameter("@AnnoI",esp.AnnoInizio),
					new SqlParameter("@AnnoF",esp.AnnoFine),
					new SqlParameter("@Qualifica",esp.Qualifica),
					new SqlParameter("@Descrizione",esp.Descrizione),
					new SqlParameter("@matr",MatrCv)
				};
				int output = DB.ExecNonQProcedure("AddEspLav", param, "GeCv");
				if (output == 0) { 
					throw new Exception("Nessuna Esperienza Inserita");
				}
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}			

		public void CaricaCV(string path) {
			throw new NotImplementedException();
		}
        //GeTime 
		public Commessa CercaCommessa(string nomeCommessa) {
			try {
				SqlParameter[] parameter = {
					new SqlParameter("@nomeCommessa", nomeCommessa)
				};			
				return DB.ExecQProcedureReader("SP_CercaCommessa", transf.TrasformInCommessa, parameter, "GeTime");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public void Compila(DateTime data, int ore, HType tipoOre, string idUtente) {
            try{ 
                SqlParameter[] parameters = {
					new SqlParameter("@giorno",data.ToString("yyyy-MM-dd")),
					new SqlParameter("@idUtente",idUtente),
					new SqlParameter("@ore", ore),
					new SqlParameter("@TipoOre", (int)tipoOre)
					};
				DB.ExecNonQProcedure("SP_Compila", parameters, "GeTime");
            } catch (SqlException e) {
                throw new Exception(e.Message);
            } catch (Exception e) {
                throw e;
            }
        }
		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,string idUtente) {
			try {
                SqlParameter[] parameters = {
					new SqlParameter("@data",data.ToString("yyyy-MM-dd")),
					new SqlParameter("@ore", ore),
					new SqlParameter("@idCommessa",idCommessa),
					new SqlParameter("@idUtente", idUtente)
					};
                DB.ExecNonQProcedure("SP_AddHLavoro", parameters, "GeTime");
			} catch (Exception e){
				throw e;
			}
		}
		public Giorno VisualizzaGiorno(DateTime data, string idUtente) {
            try {
                SqlParameter[] parameter = {
					new SqlParameter("@Data", data.ToString("yyyy-MM-dd")),               
					new SqlParameter("@IdUtente", idUtente)
				};                
                Giorno result = DB.ExecQProcedureReader("SP_VisualizzaGiorno", transf.TrasformInGiorno, parameter, "GeTime");
                if(result!=null)
                    result.Data=data;
                return result;
            } catch (Exception e) {
                throw e;
            } 
		}
		public List<Giorno> GiorniCommessa(int idCommessa,string idUtente) {
			try{
				SqlParameter[] parameter = {
					new SqlParameter("@idC", idCommessa),				
					new SqlParameter("@idU",idUtente)
				};
				return DB.ExecQProcedureReader("SP_VisualizzaCommessa", transf.TrasformInGiorni,parameter,"GeTime");
			}catch(SqlException e){
				throw new Exception(e.Message);
			}catch(Exception e){
				throw e;
			}
		}

		public void EliminaCV(CV curriculum) {
			try{
				SqlParameter[] param ={ new SqlParameter("@idcurr", curriculum.Matricola)};
				int output = DB.ExecNonQProcedure("dbo.DeleteCurriculum", param, "GeCv");
				if (output == 0) { 
					throw new Exception("Nessun curriculum eliminato!");
					}				
			}catch(SqlException e){
				throw new Exception(e.Message);
			}catch(Exception e){
				throw e;
			}
		}
		
		public List<Corso> ListaCorsi() {
		try{
			return DB.ExecQProcedureReader("ListaCorsi",transf.TrasformInCorsi, null,"GeCorsi");       
		} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<Corso> ListaCorsi(string idUtente) {
			try{
				SqlParameter[] param = { new SqlParameter ("@idStudente", idUtente) };
				return DB.ExecQProcedureReader("ListaCorsiStudenti",transf.TrasformInCorsi,param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
     
        public void ModificaCV(string nome,string cognome,int eta,string email,string residenza,string telefono,string matr) {
			try{	
				SqlParameter[] parameter = {
					new SqlParameter("@cognome", cognome),
					new SqlParameter("@matr", matr),
					new SqlParameter("@nome", nome),
					new SqlParameter("@eta", eta),
					new SqlParameter("@email", email),
					new SqlParameter("@residenza", residenza),
					new SqlParameter("@telefono", telefono)
				};
				DB.ExecNonQProcedure("ModificaCV",parameter,"GeCv");
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
        public List<CV> SearchChiava(string chiave) {
			try{
				SqlParameter[] parameter ={ new SqlParameter("@parola", chiave) };
				return DB.ExecQProcedureReader ("dbo.CercaParolaChiava", transf.TransfListCV0,parameter,"GeCv");				
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
	
		public List<CV> SearchCognome(string cognome) {
			try{
				SqlParameter[] param ={ new SqlParameter("@cognome",cognome) };
				return DB.ExecQProcedureReader("dbo.CercaCognome", transf.TransfListCV0,param,"GeCv");				
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
		public Corso SearchCorsi(int idCorso) {
			try{
				SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso)};
				return DB.ExecQProcedureReader("SearchCorso", transf.TrasformInCorso,param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}		
		public void Iscriviti(int idCorso,string idStudente) {
			try{
				SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso), new SqlParameter("@matr",idStudente)};
				DB.ExecNonQProcedure("Iscrizione",param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione) {
			try{
				SqlParameter [] param = {new SqlParameter("@descrizione", descrizione)};
				return DB.ExecQProcedureReader("SearchCorsi", transf.TrasformInCorsi,param, "GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<Corso> SearchCorsi(string descrizione,string idUtente) {
			try{
				SqlParameter [] param = {new SqlParameter("@descrizione", descrizione),
					new SqlParameter("@idStudente", idUtente)};
				return DB.ExecQProcedureReader("SearchCorsiStud", transf.TrasformInCorsi,param,"GeCorsi");
			} catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<CV> SearchEta(int eta) {
			try{
				SqlParameter[] param = { new SqlParameter("@eta",eta)};
				return DB.ExecQProcedureReader("dbo.CercaEta",transf.TransfListCV0,param, "GeCv");
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
		public List<CV> SearchRange(int etmin,int etmax) {
			try{
				SqlParameter[] parameters = {
					new SqlParameter("@e_min", etmin),
					new SqlParameter("@e_max", etmax)					
				};
				return DB.ExecQProcedureReader("dbo.CercaEtaMinMax", transf.TransfListCV0,parameters, "GeCv");
			}catch (SqlException e) {
				throw new Exception(e.Message);
			} catch (Exception e) {
				throw e;
			}
		}
	}
	[Serializable]
	internal class LezionNonModificataException : Exception {
		public LezionNonModificataException() {}
		public LezionNonModificataException(string message) : base(message) {}
		public LezionNonModificataException(string message,Exception innerException) : base(message,innerException) {}
		protected LezionNonModificataException(SerializationInfo info,StreamingContext context) : base(info,context) {}
	}
	[Serializable]
	internal class LezioneNonAggiuntaException : Exception {
		public LezioneNonAggiuntaException() {}
		public LezioneNonAggiuntaException(string message) : base(message) {}
		public LezioneNonAggiuntaException(string message,Exception innerException) : base(message,innerException){}
		protected LezioneNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context){}
	}
	[Serializable]
	internal class CorsoNonAggiuntaException : Exception {
		public CorsoNonAggiuntaException() {}
		public CorsoNonAggiuntaException(string message) : base(message) { }
		public CorsoNonAggiuntaException(string message,Exception innerException) : base(message,innerException) {}
		protected CorsoNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context) {}
	}
	}