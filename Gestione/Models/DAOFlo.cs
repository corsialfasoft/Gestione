using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Interfaces;

namespace DAO{
	public partial class DataAccesObject : IDao{
		public void ModificaCV(CV a,CV b) {
			SqlConnection connection = new SqlConnection(GetStringBuilderCV());
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("ModificaCurriculum",connection);
				command.CommandType=System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@matricolaM", System.Data.SqlDbType.NVarChar).Value=a.matricola ;
				command.Parameters.Add("@nomeM", System.Data.SqlDbType.NVarChar).Value=b.nome;
				command.Parameters.Add("@cognomeM", System.Data.SqlDbType.NVarChar).Value=b.cognome;
				command.Parameters.Add("@etaM", System.Data.SqlDbType.Int).Value=b.eta;
				command.Parameters.Add("@emailM", System.Data.SqlDbType.NVarChar).Value=b.email;
				command.Parameters.Add("@residenzaM", System.Data.SqlDbType.NVarChar).Value=b.residenza;
				command.Parameters.Add("@telefonoM", System.Data.SqlDbType.NVarChar).Value=b.telefono;
				command.ExecuteNonQuery();
				command.Dispose();
			}catch(Exception e ){
				throw e;
			}finally{
				connection.Dispose();
			}
		}
		public CV Search(string matr) {
			//return new CV {matricola = "5",nome="Massimo",cognome="franzoso",telefono="3391627441",eta=33};

			SqlConnection connection = new SqlConnection(GetStringBuilderCV());
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("GetCv",connection);
				command.CommandType=System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matr;
				SqlDataReader reader = command.ExecuteReader();
				CV c = new CV();
				while(reader.Read()){
					c.nome = reader.GetString(0);
					c.cognome = reader.GetString(1);
					c.eta = reader.GetInt32(2);
					c.matricola = reader.GetString(3);
					c.email = reader.GetString(4);
					c.residenza = reader.GetString(5);
					c.telefono = reader.GetString(6);
				}
				c.esperienze = GetEspLav(c.matricola);
				c.percorsostudi= GetPerStudi(c.matricola);
				c.competenze = GetComp(c.matricola);
				reader.Close();
				command.Dispose();
				return c;
			}catch (Exception e ){
				throw e;
			}finally{
				connection.Dispose();
			}
		}

		private List<Competenza> GetComp(string matricola) {
			SqlConnection connection = new SqlConnection(GetStringBuilderCV());
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("GetComp",connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matricola;
				List<Competenza> res = new List<Competenza>();
				SqlDataReader reader = command.ExecuteReader();
				Competenza e = new Competenza();
				while(reader.Read()){
					e.titolo = reader.GetString(0);
					e.livello = reader.GetInt32(1);
				
					res.Add(e);
				}
				reader.Close();
				command.Dispose();
				return res;
			}catch(Exception e){
				throw e;
			}finally{
				connection.Dispose();
			}
		}

		private List<PerStud> GetPerStudi(string matricola) {
			SqlConnection connection = new SqlConnection(GetStringBuilderCV());
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("GetPerStudi",connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matricola;
				List<PerStud> res = new List<PerStud>();
				SqlDataReader reader = command.ExecuteReader();
				PerStud e = new PerStud();
				while(reader.Read()){
					e.AnnoInizio = reader.GetInt32(0);
					e.AnnoFine = reader.GetInt32(1);
					e.titolo = reader.GetString(2);
					e.descrizione = reader.GetString(3);
					res.Add(e);
				}
				reader.Close();
				command.Dispose();
				return res;
			}catch(Exception e){
				throw e;
			}finally{
				connection.Dispose();
			}
		}

		private List<EspLav> GetEspLav(string matricola) {
			SqlConnection connection = new SqlConnection(GetStringBuilderCV());
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("GetEspLav",connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matricola;
				List<EspLav> res = new List<EspLav>();
				SqlDataReader reader = command.ExecuteReader();
				EspLav e = new EspLav();
				while(reader.Read()){
					e.AnnoInizio = reader.GetInt32(0);
					e.AnnoFine = reader.GetInt32(1);
					e.qualifica = reader.GetString(2);
					e.descrizione = reader.GetString(3);
					res.Add(e);
				}
				reader.Close();
				command.Dispose();
				return res;
			}catch(Exception e){
				throw e;
			}finally{
				connection.Dispose();
			}
		}

		public void AggiungiCV(CV c) {
			SqlConnection connection = new SqlConnection(GetStringBuilderCV());
			try{
				connection.Open();
				SqlCommand command = new SqlCommand("AddCv",connection);
				command.CommandType= System.Data.CommandType.StoredProcedure;
				command.Parameters.Add("@Nome",System.Data.SqlDbType.NVarChar).Value= c.nome;
				command.Parameters.Add("@Cognome",System.Data.SqlDbType.NVarChar).Value= c.cognome;
				command.Parameters.Add("@Eta",System.Data.SqlDbType.Int).Value= c.eta;
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value= c.matricola;
				command.Parameters.Add("@Residenza",System.Data.SqlDbType.NVarChar).Value= c.residenza;
				command.Parameters.Add("@Telefono",System.Data.SqlDbType.NVarChar).Value= c.telefono;
				command.ExecuteNonQuery();
				command.Dispose();
			}catch(Exception e){
				throw e;
			}finally{
				connection.Dispose();
			}
		}
		private string GetStringBuilderCV() {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.InitialCatalog = "GECV";
            return builder.ToString();
        }
	}
}