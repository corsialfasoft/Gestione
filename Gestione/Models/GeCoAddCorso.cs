using Interfaces;
using DAO;
using System;

namespace Gestione.Models {
    partial class DomainModel:IGeCo,IGeCV,IGeTime{
        DataAccesObject db = new DataAccesObject();
        public void AddCorso(Corso corso){
            try{ 
               db.AddCorso(corso);
            }catch(Exception e){ 
                throw e; 
            }
        }
    }
}