using Gestione.Controllers;
using Interfaces;
using LibreriaDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace DAO {
    public partial class DataAccesObject {
        public List<DTGiornoDMese> DettaglioMese(int anno, int mese, string idutente) {
            try {
                SqlParameter[] param = { new SqlParameter("@anno", anno), new SqlParameter("@mese",mese), new SqlParameter("@idutente", idutente)};
                List<Giorno> output = DB.ExecQProcedureReader("SP_VisualizzaMese", transf.TransfInGiorni, param, "GeTime");
                List<DTGiornoDMese> result = null; 
                if (output.Count > 0) {
                    result = output.ConvertAll(new Converter<Giorno, DTGiornoDMese>(transf.ConvertGiornoInDTGDMese));
                }else
                    result= new List<DTGiornoDMese>();
                return result;
            } catch (SqlException e) {
                throw new Exception(e.Message);
            } catch (Exception e) {
                throw e;
            }
        }
    }
}