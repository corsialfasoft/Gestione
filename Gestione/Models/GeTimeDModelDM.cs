﻿using Gestione.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gestione.Models {
    public partial class DomainModel{
        public List<DTGiornoDMese> DettaglioMese(int anno, int mese) {
            try { 
                return dao.DettaglioMese(anno, mese);
            }catch(Exception e) {
                throw e;
            }
        }
    }
}