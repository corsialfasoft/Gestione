using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using DAO;
using Gestione.Controllers;

namespace Gestione.Models{
	public partial class DomainModel : IGeCo, IGeCV, IGeTime
	{
		public void AddCorso(Corso corso) {
			throw new NotImplementedException();
		}

		public void AddLezione(int idCorso,Lezione lezione)
		{
			throw new NotImplementedException();
		}

		public void AggiungiCV(CV a)
		{
			throw new NotImplementedException();
		}

		public void CaricaCV(string path)
		{
			throw new NotImplementedException();
		}


		public void EliminaCV(CV curriculum)
		{
			throw new NotImplementedException();
		}

		
		public void Iscriviti(int idCorso,int idStudente)
		{
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi()
		{
			throw new NotImplementedException();
		}

		public List<Corso> ListaCorsi(int idUtente)
		{
			throw new NotImplementedException();
		}

		public void ModificaCV(CV a,CV b)
		{
			throw new NotImplementedException();
		}

		public CV Search(string id)
		{
			throw new NotImplementedException();
		}

		public List<CV> SearchChiava(string chiava)
		{
			throw new NotImplementedException();
		}

		public List<CV> SearchCognome(string cognome)
		{
			throw new NotImplementedException();
		}

		public Corso SearchCorsi(int idCorso)
		{
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione)
		{
			throw new NotImplementedException();
		}

		public List<Corso> SearchCorsi(string descrizione,int idUtente)
		{
			throw new NotImplementedException();
		}

		public List<CV> SearchEta(int eta)
		{
			throw new NotImplementedException();
		}

		public List<CV> SearchRange(int etmin,int etmax)
		{
			throw new NotImplementedException();
		}

		public DTGGiorno VisualizzaGiorno(DateTime data,string idUtente) {
            Giorno giornoInterface = new DataAccesObject().VisualizzaGiorno(data, idUtente);
            if (giornoInterface!=null) {
                DTGGiorno DTgiorno = new DTGGiorno();
                DTgiorno.data = giornoInterface.Data;
                DTgiorno.OrePermesso = giornoInterface.HPermesso;
                DTgiorno.OreMalattia = giornoInterface.HMalattia;
                DTgiorno.OreFerie = giornoInterface.HFerie;
                foreach(OreCommessa orecommessa in giornoInterface.OreLavorate) {
                    OreLavorate orelavorate = new OreLavorate();
                    orelavorate.nome = orecommessa.Nome;
                    orelavorate.oreGiorno = orecommessa.Ore;
                    orelavorate.descrizione = orecommessa.Descrizione;
                    DTgiorno.OreLavorate.Add(orelavorate);
                }
                DTgiorno.TotOreLavorate = giornoInterface.TotOreLavorate();
                return DTgiorno;
            }
            return null;
		}

	}
}