using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FaktureStavkeApp.Models
{
    public class FakturaStavka
    {
        [Key]
        public int FakturaStavkaId { get; set; }
        public string Opis { get; set; }
        public int KolicinaProdaneStavke { get; set; }
        public double JedinicnaCijenaPDV { get; set; }

        [ForeignKey("Faktura")]
        public int FakturaID { get; set; }
        public Fakture Faktura { get; set; }
    }
}