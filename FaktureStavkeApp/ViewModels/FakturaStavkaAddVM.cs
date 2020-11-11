using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaktureStavkeApp.ViewModels
{
    public class FakturaStavkaAddVM
    {
        public string Opis { get; set; }
        public int KolicinaProdaneStavke { get; set; }
        public double JedinicnaCijenaPDV { get; set; }

        public int FakturaID { get; set; }
    }
}