using FaktureStavkeApp.Models;
using FaktureStavkeApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FaktureStavkeApp.Controllers
{
    public class FaktureController : Controller
    {
        // GET: Fakture
        ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult Index()
        {
            List<Fakture> list = db.Fakture.ToList();


            List<FakturaIndexVM> model = db.Fakture.Select(
                a => new FakturaIndexVM
                {
                    FakturaID = a.FakturaID,
                    BrojFakture = a.BrojFakture,
                    DatumDospijeca = a.DatumDospijeca,
                    DatumStvaranja=a.DatumStvaranja,
                    UkupnaCijenaBezPDV = db.FakturaStavke.Where(x => x.FakturaID == a.FakturaID).Sum(x => (double?)x.JedinicnaCijenaPDV * x.KolicinaProdaneStavke) ?? 0,
                    UkupnaCijenaPDV = (db.FakturaStavke.Where(x => x.FakturaID == a.FakturaID).Sum(x => (double?)x.JedinicnaCijenaPDV * x.KolicinaProdaneStavke) ?? 0)*(a.IznosPorezaUPostotcima/100)+((db.FakturaStavke.Where(x => x.FakturaID == a.FakturaID).Sum(x => (double?)x.JedinicnaCijenaPDV * x.KolicinaProdaneStavke) ?? 0)
                    )
                 ,
                    Korisnik = a.Korisnik.UserName,
                    PrimateljRacuna=a.PrimateljRacuna

                }
                ).ToList();

            return View(model);
        }
 

        // GET: Fakture/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fakture fakture = db.Fakture.Find(id);
            var stavke = db.FakturaStavke.Where(x => x.FakturaID == id).ToList();




            if (fakture == null)
            {
                return HttpNotFound();
            }
            FakturaStavkaDetaljiVM model = null;
            if (fakture != null)
            {
                var korisnik = db.Users.Find(fakture.KorisnikId);
                model = new FakturaStavkaDetaljiVM
                {
                    BrojFakture = fakture.BrojFakture,
                    DatumDospijeca = fakture.DatumDospijeca.ToShortDateString(),
                    

                    PrimateljRacuna=fakture.PrimateljRacuna,
                    Korisnik = korisnik.UserName,
                    Stavke = stavke.Select(
                  s => new FakturaStavka
                  {
                      JedinicnaCijenaPDV = s.JedinicnaCijenaPDV,
                      KolicinaProdaneStavke = s.KolicinaProdaneStavke,
                      Opis = s.Opis,
                      FakturaStavkaId = s.FakturaStavkaId
                  }).ToList(),
                    CijenaBezPDV = stavke.Sum(s => s.KolicinaProdaneStavke * s.JedinicnaCijenaPDV),
                    CijenaPDV = (stavke.Sum(s=>s.KolicinaProdaneStavke*s.JedinicnaCijenaPDV)*(fakture.IznosPorezaUPostotcima/100))+ stavke.Sum(s => s.KolicinaProdaneStavke * s.JedinicnaCijenaPDV)



                };
            }
            return View(model);
        }
        
        public List<PDV>GetPDVTip()
        {
            var list = new List<PDV>();


            list.Add(new PDV
            {
                IznosPDV = 17,
                Naziv = "PDVBiH",
                PDVe = PDVenum.PDVBIH
            });
            list.Add(new PDV
            {
                IznosPDV = 25,
                Naziv = "PDVHR",
                PDVe = PDVenum.PDVHR
            });
            return list;
        }

        // GET: Fakture/Create
        [Authorize]
        public ActionResult Create()
        {
            FakturaAddVM model = new FakturaAddVM()
            {
                listPDV = new List<SelectListItem>()
            };

            var list = GetPDVTip();
            foreach (var i in list)
            {
                model.listPDV.Add(new SelectListItem
                {
                    Text=i.Naziv,
                    Value = i.PDVe.ToString()
                });
            }
            return View(model);
        }

        // POST: Fakture/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DatumDospijeca,PrimateljRacuna")] FakturaAddVM fakture)
        {
          
            if (ModelState.IsValid)
            {
                var temp = new Fakture
                {
                    BrojFakture = fakture.BrojFakture,
                    DatumDospijeca = fakture.DatumDospijeca,
                    KorisnikId = User.Identity.GetUserId<int>(),
                    PrimateljRacuna=fakture.PrimateljRacuna,
                    DatumStvaranja=DateTime.Now


                };
                var pdvlist = GetPDVTip();
                var pdv = (PDVenum)fakture.intPDV;
                PDV p= pdvlist.Where(x => x.PDVe == pdv).FirstOrDefault();
                temp.IznosPorezaUPostotcima = p.IznosPDV;
            
                db.Fakture.Add(temp);
                db.SaveChanges();
                var t=db.Fakture.Find(temp.FakturaID);
                t.BrojFakture = t.FakturaID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fakture);
        }

        // GET: Fakture/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fakture fakture = db.Fakture.Find(id);
        
            if (fakture == null)
            {
                return HttpNotFound();
            }
            return View(fakture);
        }

        // POST: Fakture/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FakturaID,BrojFakture,DatumDospijeca,KorisnikId,IznosPorezaUPostotcima,PrimateljRacuna")] Fakture fakture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fakture);
        }

        // GET: Fakture/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fakture fakture = db.Fakture.Find(id);
            if (fakture == null)
            {
                return HttpNotFound();
            }
            return View(fakture);
        }

        // POST: Fakture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fakture fakture = db.Fakture.Find(id);
            db.Fakture.Remove(fakture);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
