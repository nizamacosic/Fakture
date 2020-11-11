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
                    UkupnaCijenaBezPDV = db.FakturaStavke.Where(x => x.FakturaID == a.FakturaID).Sum(x => (double?)x.JedinicnaCijenaPDV * x.KolicinaProdaneStavke) ?? 0,
                    UkupnaCijenaPDV = 0 //dodaj pdv,
                 ,
                    Korisnik = a.Korisnik.UserName

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
                var korisnik = db.Users.Find(fakture.Id);
                model = new FakturaStavkaDetaljiVM
                {
                    BrojFakture = fakture.BrojFakture,
                    DatumDospijeca = fakture.DatumDospijeca.ToShortDateString(),

                    CijenaPDV = stavke.Sum(k => k.JedinicnaCijenaPDV),
                    Korisnik = korisnik.UserName,
                    Stavke = stavke.Select(
                  s => new FakturaStavka
                  {
                      JedinicnaCijenaPDV = s.JedinicnaCijenaPDV,
                      KolicinaProdaneStavke = s.KolicinaProdaneStavke,
                      Opis = s.Opis,
                      FakturaStavkaId = s.FakturaStavkaId
                  }).ToList(),
                    CijenaBezPDV = stavke.Sum(s => s.KolicinaProdaneStavke * s.JedinicnaCijenaPDV)



                };
            }
            return View(model);
        }

        // GET: Fakture/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fakture/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrojFakture,DatumDospijeca")] FakturaAddVM fakture)
        {
            if (ModelState.IsValid)
            {
                var temp = new Fakture
                {
                    BrojFakture = fakture.BrojFakture,
                    DatumDospijeca = fakture.DatumDospijeca,
                    Id = User.Identity.GetUserId<int>()


                };
                db.Fakture.Add(temp);
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
        public ActionResult Edit([Bind(Include = "FakturaID,BrojFakture,DatumDospijeca,Id")] Fakture fakture)
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
