using FaktureStavkeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaktureStavkeApp.ViewModels
{
    public class FakturaStavkaDetaljiVM
    {
        public int FakturaID { get; set; }
        public int BrojFakture { get; set; }
        public string DatumDospijeca { get; set; }
        public double CijenaBezPDV { get; set; }
        public double CijenaPDV { get; set; }
        public string Korisnik { get; set; }
        public string PrimateljRacuna { get; set; }
        public List<FakturaStavka> Stavke { get; set; }
        
    }
}