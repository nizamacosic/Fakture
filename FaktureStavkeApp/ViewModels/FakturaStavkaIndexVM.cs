using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaktureStavkeApp.ViewModels
{
    public class FakturaStavkaIndexVM
    {
        public int FakturaStavkaId { get; set; }
        public string Opis { get; set; }
        public int KolicinaProdaneStavke { get; set; }
        public double JedinicnaCijenabezPDV { get; set; }
        public double UkupnaCijenabezPDV { get { return KolicinaProdaneStavke * JedinicnaCijenabezPDV; } }

        public int FakturaID { get; set; }
    }
}