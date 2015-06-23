using AVACOM_Online_Testiranje.Areas.Admin.Models;
using AVACOM_Online_Testiranje.DAL;
using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.Admin.Controllers
{
    public class UnosOdgovoraController : Controller
    {
        private MojContext ctx = new MojContext();

        //
        // GET: /Admin/UnosOdgovora/
        public ActionResult Index(int? odgovorId)
        {
            List<OdgovorPrikazi.OdgovorInfo> odgovor = ctx.Odgovori
                 .Where(x => !odgovorId.HasValue || x.Id == odgovorId)
                 .Select(x => new OdgovorPrikazi.OdgovorInfo()
                 {
                     Tekst = x.Tekst,
                     Tacan = x.Tacan,
                     Id = x.Id,
                     Pitanje_Naziv = x.Pitanje.Tekst

                 })
                 .ToList();

            OdgovorPrikazi Model = new OdgovorPrikazi
            {
                odgovori = odgovor,
                pitanja = Ucitajpitanja()
            };

            return View("Index", Model);
        }


        public ActionResult Obrisi(int OdgovorId)
        {
            Odgovor x = ctx.Odgovori.Find(OdgovorId);
            ctx.Odgovori.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

     
        public ActionResult Dodaj()
        {
            OdgovorEdit Model = new OdgovorEdit();

            Model.pitanjestavke = Ucitajpitanja();
            return View("Uredi", Model);
        }

        public ActionResult Uredi(int odgovorId)
        {
            Odgovor odgovor = ctx.Odgovori
                .Where(x => x.Id == odgovorId)
                .Single();

            OdgovorEdit Model = new OdgovorEdit
            {
                Id = odgovor.Id,
                Tekst = odgovor.Tekst,
                Tacan = odgovor.Tacan,
                PitanjeId = odgovor.PitanjeId,
                pitanjestavke = Ucitajpitanja()
            };


            return View("Uredi", Model);
        }

        public ActionResult Snimi(OdgovorEdit odgovor)
        {
            if (!ModelState.IsValid)
            {
                odgovor.pitanjestavke = Ucitajpitanja();
                return View("Uredi", odgovor);
            }

            Odgovor odgovorDB;
            if (odgovor.Id == 0)
            {
                odgovorDB = new Odgovor();
                //pitanjeDB.Oblast = new Oblast();
                ctx.Odgovori.Add(odgovorDB);
            }
            else
            {
                odgovorDB = ctx.Odgovori.Where(s => s.Id == odgovor.Id).FirstOrDefault();
            }



            odgovorDB.Tekst = odgovor.Tekst;
            odgovorDB.Tacan = odgovor.Tacan;
            odgovorDB.PitanjeId = odgovor.PitanjeId;
           

           


            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> Ucitajpitanja()
        {
            var pitanja = new List<SelectListItem>();
            pitanja.Add(new SelectListItem { Value = null, Text = "(sva pitanja)" });
            pitanja.AddRange(ctx.Pitanja
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Tekst }).ToList());
            return pitanja;
        }


       



	}
}