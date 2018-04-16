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
				SqlCommand command = new SqlCommand("ModificaCurriculum",connection) {
					CommandType = System.Data.CommandType.StoredProcedure
				};
				command.Parameters.Add("@matricolaM", System.Data.SqlDbType.NVarChar).Value=a.Matricola ;
				command.Parameters.Add("@nomeM", System.Data.SqlDbType.NVarChar).Value=b.Nome;
				command.Parameters.Add("@cognomeM", System.Data.SqlDbType.NVarChar).Value=b.Cognome;
				command.Parameters.Add("@etaM", System.Data.SqlDbType.Int).Value=b.Eta;
				command.Parameters.Add("@emailM", System.Data.SqlDbType.NVarChar).Value=b.Email;
				command.Parameters.Add("@residenzaM", System.Data.SqlDbType.NVarChar).Value=b.Residenza;
				command.Parameters.Add("@telefonoM", System.Data.SqlDbType.NVarChar).Value=b.Telefono;
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
				SqlCommand command = new SqlCommand("GetCv",connection) {
					CommandType = System.Data.CommandType.StoredProcedure
				};
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matr;
				SqlDataReader reader = command.ExecuteReader();
				CV c = new CV();
				while(reader.Read()){
					c.Nome = reader.GetString(0);
					c.Cognome = reader.GetString(1);
					c.Eta = reader.GetInt32(2);
					c.Matricola = reader.GetString(3);
					c.Email = reader.GetString(4);
					c.Residenza = reader.GetString(5);
					c.Telefono = reader.GetString(6);
				}
				c.Esperienze = GetEspLav(c.Matricola);
				c.Percorsostudi= GetPerStudi(c.Matricola);
				c.Competenze = GetComp(c.Matricola);
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
				SqlCommand command = new SqlCommand("GetComp",connection) {
					CommandType = System.Data.CommandType.StoredProcedure
				};
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matricola;
				List<Competenza> res = new List<Competenza>();
				SqlDataReader reader = command.ExecuteReader();
				Competenza e = new Competenza();
				while(reader.Read()){
					e.Titolo = reader.GetString(0);
					e.Livello = reader.GetInt32(1);
				
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
				SqlCommand command = new SqlCommand("GetPerStudi",connection) {
					CommandType = System.Data.CommandType.StoredProcedure
				};
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matricola;
				List<PerStud> res = new List<PerStud>();
				SqlDataReader reader = command.ExecuteReader();
				PerStud e = new PerStud();
				while(reader.Read()){
					e.AnnoInizio = reader.GetInt32(0);
					e.AnnoFine = reader.GetInt32(1);
					e.Titolo = reader.GetString(2);
					e.Descrizione = reader.GetString(3);
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
				SqlCommand command = new SqlCommand("GetEspLav",connection) {
					CommandType = System.Data.CommandType.StoredProcedure
				};
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value=matricola;
				List<EspLav> res = new List<EspLav>();
				SqlDataReader reader = command.ExecuteReader();
				EspLav e = new EspLav();
				while(reader.Read()){
					e.AnnoInizio = reader.GetInt32(0);
					e.AnnoFine = reader.GetInt32(1);
					e.Qualifica = reader.GetString(2);
					e.Descrizione = reader.GetString(3);
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
				SqlCommand command = new SqlCommand("AddCv",connection) {
					CommandType = System.Data.CommandType.StoredProcedure
				};
				command.Parameters.Add("@Nome",System.Data.SqlDbType.NVarChar).Value= c.Nome;
				command.Parameters.Add("@Cognome",System.Data.SqlDbType.NVarChar).Value= c.Cognome;
				command.Parameters.Add("@Eta",System.Data.SqlDbType.Int).Value= c.Eta;
				command.Parameters.Add("@Matricola",System.Data.SqlDbType.NVarChar).Value= c.Matricola;
				command.Parameters.Add("@Residenza",System.Data.SqlDbType.NVarChar).Value= c.Residenza;
				command.Parameters.Add("@Telefono",System.Data.SqlDbType.NVarChar).Value= c.Telefono;
				command.ExecuteNonQuery();
				command.Dispose();
			}catch(Exception e){
				throw e;
			}finally{
				connection.Dispose();
			}
		}
		private string GetStringBuilderCV() {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder {
				DataSource = @"(localdb)\MSSQLLocalDB",
				InitialCatalog = "GECV"
			};
			return builder.ToString();
        }
	}
}