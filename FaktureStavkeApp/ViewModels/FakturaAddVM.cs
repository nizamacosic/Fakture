using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FaktureStavkeApp.ViewModels
{
    public enum PDVenum
    {
        [Display(Name="PDVBIH")]
        PDVBIH,
        [Display(Name="PDVHR")]
        PDVHR
    }
    public class PDV
    {
        public PDVenum PDVe { get; set; }
        public double IznosPDV { get; set; }
        public string Naziv { get; set; }
    }
    public class FakturaAddVM
    {
     
        public int BrojFakture { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public int Id { get; set; }
        public string PrimateljRacuna { get; set; }

        public List<SelectListItem> listPDV { get; set; }
        public int intPDV { get; set; }

    
        
    }
}