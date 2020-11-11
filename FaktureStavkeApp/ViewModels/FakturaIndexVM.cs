using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaktureStavkeApp.ViewModels
{
    public class FakturaIndexVM
    {
        public int FakturaID { get; set; }
        public int BrojFakture { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public DateTime DatumStvaranja{ get; set; }
        public double UkupnaCijenaBezPDV { get; set; }
        public double UkupnaCijenaPDV { get; set; }
        public string Korisnik { get; set; }
        public string PrimateljRacuna { get; set; }
    }
}