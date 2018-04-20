using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Interfaces;
using LibreriaDB;

namespace DAO{
	public interface IDao{
		void ModificaCV(CV a, CV b); //modifica un curriculum nel db
		void AggiungiCV(CV a); //quando sei loggato, puoi aggiungere un curriculum nel db
		void CaricaCV(string path); //quando non sei loggato, puoi spedire un curriuculum
		CV Search(string id); //search di un curriculum per id di un curriculum
		List<CV> SearchChiava(string chiava); //search generale per parole chiava 
		List<CV> SearchEta(int eta); //search solo per quella precisa età
		List<CV> SearchRange(int etmin, int etmax); //search per un range di età minimo e massimo
		void EliminaCV(CV curriculum); //Elimina un CV dal db
		List<CV> SearchCognome(string cognome); //Ricerca solo per cognome
	
	
	
		void CompilaHLavoro(DateTime data, int ore, int idCommessa, int idUtente);
		void Compila(DateTime data, int ore, HType tipoOre, int idUtente);
		Giorno VisualizzaGiorno(DateTime data, int idUtente);
		List<Giorno> GiorniCommessa(int idCommessa, int idUtente);
		Commessa CercaCommessa(string nomeCommessa);
        
        //Aggiungi nuovo corso. Lo puo fare solo l'admin
        void AddCorso(Corso corso);
        //Aggiungi una lezione a un determinato corso. Lo puo fare solo il prof
        void AddLezione(int idCorso, Lezione lezione);
		void ModLezione(int idLezione,Lezione lezione);
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
			return DB.ExecQProcedureReader("ListaLezioni",transf.TrasformInLezione,param);
		}
		public void AddCorso(Corso corso) {
			SqlParameter[] param = {
				new SqlParameter("@nome", corso.Nome),
				new SqlParameter("@descrizione", corso.Descrizione),
				new SqlParameter("@dInizio", corso.Inizio),
				new SqlParameter("@dFine", corso.Fine)
			};
			int RowAffected = DB.ExecNonQProcedure("AddCorso", param);
			if(RowAffected == 0){
				throw new CorsoNonAggiuntaException("Corso non aggiunto") ;
			}
		}
		public void AddLezione(int idCorso,Lezione lezione) {
			SqlParameter[] param = {
				new SqlParameter ("@idCorsi", idCorso),
				new SqlParameter ("@nome", lezione.Nome),
				new SqlParameter("@descrizione", lezione.Descrizione),
				new SqlParameter("@durata", lezione.Durata)
			};
			int RowAffected = DB.ExecNonQProcedure("AddLezione", param);
			if(RowAffected == 0){
				throw new LezioneNonAggiuntaException("Lezione non aggiunta") ;
			}
		}
			public void ModLezione(int idLezione,Lezione lezione)
		{
			SqlParameter[] param = {
				new SqlParameter("@idLezione",idLezione),
				new SqlParameter("@nome",lezione.Nome),
				new SqlParameter("@descrizione",lezione.Descrizione),
				new SqlParameter("@durata",lezione.Durata)
			};
			int RowAffected =DB.ExecNonQProcedure("ModLezione",param,"GeCorsi",@"(localdb)\MSSQLLocalDB");
			if (RowAffected == 0) {
				throw new LezionNonModificataException("Non hai modificato la lezione");
			}
		}
		public void AggiungiCV(CV a) {
			throw new NotImplementedException();
		}
		public void CaricaCV(string path) {
			throw new NotImplementedException();
		}
		public Commessa CercaCommessa(string nomeCommessa) {
			throw new NotImplementedException();
		}
		public void Compila(DateTime data,int ore,HType tipoOre,int idUtente) {
			throw new NotImplementedException();
		}
		public void CompilaHLavoro(DateTime data,int ore,int idCommessa,int idUtente) {
			throw new NotImplementedException();
		}
		public void EliminaCV(CV curriculum) {
			throw new NotImplementedException();
		}
		public List<Giorno> GiorniCommessa(int idCommessa,int idUtente) {
			throw new NotImplementedException();
		}
		public List<Corso> ListaCorsi() {		   
			return DB.ExecQProcedureReader("ListaCorsi",transf.TrasformInListaCorso, null);       
		}
		public List<Corso> ListaCorsi(string idUtente) {
			SqlParameter[] param = { new SqlParameter ("@idStudente", idUtente) };
			return DB.ExecQProcedureReader("ListaCorsiStudenti",transf.TrasformInListaCorso,param);
		}
		public void ModificaCV(CV a,CV b) {
			throw new NotImplementedException();
		}
		public CV Search(string id) {
			throw new NotImplementedException();
		}
		public List<CV> SearchChiava(string chiava) {
			throw new NotImplementedException();
		}
		public List<CV> SearchCognome(string cognome) {
			throw new NotImplementedException();
		}      
		public Corso SearchCorsi(int idCorso) {
			SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso)};
			return DB.ExecQProcedureReader("SearchCorso", transf.TrasformInCorso,param);
		}		
		public void Iscriviti(int idCorso,string idStudente) {
			SqlParameter[] param = {new SqlParameter("@IdCorso",idCorso), new SqlParameter("@matr",idStudente)};
			DB.ExecNonQProcedure("Iscrizione",param);
		}
		public List<Corso> SearchCorsi(string descrizione) {
			SqlParameter [] param = {new SqlParameter("@descrizione", descrizione)};
			return DB.ExecQProcedureReader("SearchCorsi", transf.TrasformInListaCorso,param);
		}
		public List<Corso> SearchCorsi(string descrizione,string idUtente)  {
			SqlParameter [] param = {new SqlParameter("@descrizione", descrizione),
				new SqlParameter("@idStudente", idUtente)};
			return DB.ExecQProcedureReader("SearchCorsiStud", transf.TrasformInListaCorso,param);
		}
		public List<CV> SearchEta(int eta) {
			throw new NotImplementedException();
		}
		public List<CV> SearchRange(int etmin,int etmax) {
			throw new NotImplementedException();
		}
		public Giorno VisualizzaGiorno(DateTime data,int idUtente) {
			throw new NotImplementedException();
		}

	

		[Serializable]
		private class LezioneNonAggiuntaException : Exception {
			public LezioneNonAggiuntaException() {}
			public LezioneNonAggiuntaException(string message) : base(message) {}
			public LezioneNonAggiuntaException(string message,Exception innerException) : base(message,innerException){}
			protected LezioneNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context){
			}
		}
		[Serializable]
		private class CorsoNonAggiuntaException : Exception {
			public CorsoNonAggiuntaException() {}
			public CorsoNonAggiuntaException(string message) : base(message) { }
			public CorsoNonAggiuntaException(string message,Exception innerException) : base(message,innerException) {}
			protected CorsoNonAggiuntaException(SerializationInfo info,StreamingContext context) : base(info,context) {
			}
		}
	}

	[Serializable]
	internal class LezionNonModificataException : Exception
	{
		public LezionNonModificataException()
		{
		}

		public LezionNonModificataException(string message) : base(message)
		{
		}

		public LezionNonModificataException(string message,Exception innerException) : base(message,innerException)
		{
		}

		protected LezionNonModificataException(SerializationInfo info,StreamingContext context) : base(info,context)
		{
		}
	}
}