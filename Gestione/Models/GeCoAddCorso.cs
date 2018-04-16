using Interfaces;
using DAO;

namespace Gestione.Models {
    partial class DomainModel:IGeCo,IGeCV,IGeTime{
        DataAccesObject db = new DataAccesObject();
        public void AddCorso(Corso corso){
            try{ 
               db.AddCorso(corso);
            }catch(System.NotImplementedException){ 
                throw new System.Exception();    
            }
        }
    }
}