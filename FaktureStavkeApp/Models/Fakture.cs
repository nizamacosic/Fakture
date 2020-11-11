using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FaktureStavkeApp.Models
{
    public class Fakture
    {
        [Key]
        public int FakturaID { get; set; }
        public int BrojFakture { get; set; }
        public DateTime DatumDospijeca { get; set; }

        //public string KorisnikImePrezime { get; set; }
        [ForeignKey("Korisnik")]
        public int Id { get; set; }
        public ApplicationUser Korisnik { get; set; }
    }
}