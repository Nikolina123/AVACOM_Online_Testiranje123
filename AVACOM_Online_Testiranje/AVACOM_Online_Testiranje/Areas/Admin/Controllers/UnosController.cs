using AVACOM_Online_Testiranje.Areas.Admin.Models;
using AVACOM_Online_Testiranje.DAL;
using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.Admin.Controllers
{
    public class UnosController : Controller
    {


        private MojContext ctx = new MojContext();

        public ActionResult Index(int? oblastId)
        {
            List<PitanjePrikaziVM.PitanjeInfo> pitanja = ctx.Pitanja
                .Where(x => !oblastId.HasValue || x.OblastId == oblastId)
                .Select(x => new PitanjePrikaziVM.PitanjeInfo()
                {
                   Tekst = x.Tekst,
                   Bod = x.Bod,
                    Id = x.Id,
                    VrtaPitanja_Naziv = x.VrstaPitanja.Naziv,
                    Oblst_Naziv = x.Oblast.Naziv
                    
                })
                .ToList();

            PitanjePrikaziVM Model = new PitanjePrikaziVM
            {
                Pitanja = pitanja,
                oblastiStavke = UcitajOblasti()
            };

            return View("Index", Model);
        }

       







        public ActionResult Obrisi(int pitanjeId)
        {
            Pitanje x = ctx.Pitanja.Find(pitanjeId);
            ctx.Pitanja.Remove(x);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Dodaj()
        {
            PitanjeEditVM Model = new PitanjeEditVM();

            Model.OblastiStavke = UcitajOblasti();
            return View("Uredi", Model);
        }

        public ActionResult Uredi(int pitanjeId)
        {
            Pitanje pitanje = ctx.Pitanja
                .Where(x => x.Id == pitanjeId)
                .Single();

            PitanjeEditVM Model = new PitanjeEditVM
            {
                Id = pitanje.Id,
                Tekst = pitanje.Tekst,
                Bod = pitanje.Bod,
                VrstaPitanjaId = pitanje.VrstaPitanjaId,
                OblastId = pitanje.OblastId,
                OblastiStavke = UcitajOblasti(),
                VrstaPitanjaStavke = UcitajVrstuPitanja()

            };


            return View("Uredi", Model);
        }

        public ActionResult Snimi(PitanjeEditVM pitanje)
        {
            if (!ModelState.IsValid)
            {
                pitanje.OblastiStavke = UcitajOblasti();
                return View("Uredi", pitanje);
            }

            Pitanje pitanjeDB;
            if (pitanje.Id == 0)
            {
                pitanjeDB = new Pitanje();
                //pitanjeDB.Oblast = new Oblast();
                ctx.Pitanja.Add(pitanjeDB);
            }
            else
            {
                pitanjeDB = ctx.Pitanja.Where(s => s.Id == pitanje.Id).FirstOrDefault();
            }



            pitanjeDB.Tekst = pitanje.Tekst;
            pitanjeDB.Bod = pitanje.Bod;
            pitanjeDB.OblastId = pitanje.OblastId;
            pitanjeDB.VrstaPitanjaId = pitanje.VrstaPitanjaId;

           


            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> UcitajOblasti()
        {
            var oblasti = new List<SelectListItem>();
            oblasti.Add(new SelectListItem { Value = null, Text = "(sve oblasti)" });
            oblasti.AddRange(ctx.Oblasti
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv  }).ToList());
            return oblasti;
        }


        private List<SelectListItem> UcitajVrstuPitanja()
        {
            var vrsta = new List<SelectListItem>();
            vrsta.Add(new SelectListItem { Value = null, Text = "(vrste pitanja)" });

            vrsta.AddRange(ctx.VrstePitanja
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Naziv }).ToList());
            return vrsta;
        }



       



	}
}