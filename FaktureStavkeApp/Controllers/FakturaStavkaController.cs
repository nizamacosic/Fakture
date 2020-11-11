using FaktureStavkeApp.Models;
using FaktureStavkeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FaktureStavkeApp.Controllers
{
    public class FakturaStavkaController : Controller
    {
        // GET: FakturaStavka

        // GET: FakturaStavka
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var fakturaStavka = db.FakturaStavke;
            return View(fakturaStavka.ToList());
        }

        public ActionResult Detalji(int id)
        {
            Fakture fa = db.Fakture.Find(id);
            List<FakturaStavka> stavke = db.FakturaStavke.Where(x => x.FakturaID == id).ToList();


            var user = db.Users.Find(fa.KorisnikId);
            var model = new FakturaStavkaDetaljiVM
            {
                BrojFakture = fa.BrojFakture,
                DatumDospijeca = fa.DatumDospijeca.ToShortDateString(),
                PrimateljRacuna=fa.PrimateljRacuna,

                CijenaPDV = stavke.Sum(k => k.JedinicnaCijenaPDV),
                Korisnik = user.UserName,
                Stavke = stavke.Select(
                    s => new FakturaStavka
                    {
                        JedinicnaCijenaPDV = s.JedinicnaCijenaPDV,
                        KolicinaProdaneStavke = s.KolicinaProdaneStavke,
                        Opis = s.Opis,

                    }).ToList()


            };


            return View(model);
        }




        // GET: FakturaStavka/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturaStavka fakturaStavka = db.FakturaStavke.Find(id);
            if (fakturaStavka == null)
            {
                return HttpNotFound();
            }
            return View(fakturaStavka);
        }

        // GET: FakturaStavka/Create
        public ActionResult Create(int id)
        {
            //ViewBag.FakturaID = new SelectList(db.Faktura, "FakturaID", "KorisnikImePrezime");
            //ViewBag.FakturaID = id;
            FakturaStavkaAddVM model = new FakturaStavkaAddVM()
            {
                FakturaID = id
            };
            return View(model);
        }

        // POST: FakturaStavka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FakturaStavkaId,Opis,KolicinaProdaneStavke,JedinicnaCijenaPDV,FakturaID")] FakturaStavkaAddVM fakturaStavka)
        {
            if (ModelState.IsValid)
            {
                var temp = new FakturaStavka
                {
                    FakturaID = fakturaStavka.FakturaID,
                    JedinicnaCijenaPDV = fakturaStavka.JedinicnaCijenaPDV,
                    KolicinaProdaneStavke = fakturaStavka.KolicinaProdaneStavke,
                    Opis = fakturaStavka.Opis,

                };
                db.FakturaStavke.Add(temp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FakturaID = new SelectList(db.Fakture, "FakturaID", "KorisnikImePrezime", fakturaStavka.FakturaID);
            return View(fakturaStavka);
        }

        // GET: FakturaStavka/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturaStavka fakturaStavka = db.FakturaStavke.Find(id);
            if (fakturaStavka == null)
            {
                return HttpNotFound();
            }
            ViewBag.FakturaID = new SelectList(db.Fakture, "FakturaID", "KorisnikImePrezime", fakturaStavka.FakturaID);
            return View(fakturaStavka);
        }

        // POST: FakturaStavka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FakturaStavkaId,Opis,KolicinaProdaneStavke,JedinicnaCijenaPDV,FakturaID")] FakturaStavka fakturaStavka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakturaStavka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FakturaID = new SelectList(db.Fakture, "FakturaID", "KorisnikImePrezime", fakturaStavka.FakturaID);
            return View(fakturaStavka);
        }

        // GET: FakturaStavka/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FakturaStavka fakturaStavka = db.FakturaStavke.Find(id);
            if (fakturaStavka == null)
            {
                return HttpNotFound();
            }
            return View(fakturaStavka);
        }

        // POST: FakturaStavka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FakturaStavka fakturaStavka = db.FakturaStavke.Find(id);
            db.FakturaStavke.Remove(fakturaStavka);
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
