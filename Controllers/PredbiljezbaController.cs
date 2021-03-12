using Seminarski_rad_Olujic_AnaMaria.Models;
using Seminarski_rad_Olujic_AnaMaria.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seminarski_rad_Olujic_AnaMaria.Controllers
{
    [Authorize]
    public class PredbiljezbaController : Controller
    {

        #region General- instanciranje DbService class

        private readonly DbService dbServis;
        public PredbiljezbaController()
        {
            this.dbServis = new DbService();
        }
        #endregion

        #region Predbiljezbe
        
        public ActionResult KreirajPredbiljezbu(int id)
        {
            var seminar = dbServis.DohvatiSeminare().FirstOrDefault(x => x.Id == id);
            ViewBag.Naziv = seminar.Naziv;
            return View();
        }

        [HttpPost]
        public ActionResult KreirajPredbiljezbu(PredbiljezbaModel predbiljezba,int id )
        {
            var seminar = dbServis.DohvatiSeminare().FirstOrDefault(x => x.Id == id);
            predbiljezba.Seminar = seminar;
            predbiljezba.Datum = DateTime.Now;
            predbiljezba.Statusi = Status.Neobrađeno;
            dbServis.KreirajPredbiljezbu(predbiljezba);
            return RedirectToAction("PopisSeminaraZaPredbiljezbu", "Seminar");
        }


        //get: popis predbiljezbi
        //search: naziv, ime, prezime polaznika
        //filter: datum, status
        //sort: naziv, datum, status,
        public ActionResult PopisPredbiljezbi(string searchString, string predbiljezbaStatus, DateTime? searchDate, string sortOrder)
        {

            var predbiljezbe = dbServis.DohvatiPredbiljezbe();

            ViewBag.SortNaziv = sortOrder == "naziv_asc" ? "naziv_desc" : "naziv_asc";
            ViewBag.SortDatum = sortOrder == "datum_asc" ? "datum_desc" : "datum_asc";
            ViewBag.SortStatus = sortOrder == "status_asc" ? "status_desc" : "status_asc";

            if (!String.IsNullOrEmpty(searchString))
            {
                predbiljezbe = predbiljezbe.Where(s => s.Seminar.Naziv.Contains(searchString)
                || s.Ime.Contains(searchString)
                || s.Prezime.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "naziv_desc":
                    predbiljezbe = predbiljezbe.OrderByDescending(s => s.Seminar.Naziv);
                    break;
                case "naziv_asc":
                    predbiljezbe = predbiljezbe.OrderBy(s => s.Seminar.Naziv);
                    break;
                case "datum_desc":
                    predbiljezbe = predbiljezbe.OrderByDescending(x => x.Datum);
                    break;
                case "datum_asc":
                    predbiljezbe = predbiljezbe.OrderBy(s => s.Datum);
                    break;
                case "status_desc":
                    predbiljezbe = predbiljezbe.OrderByDescending(x => x.Statusi);
                    break;
                case "status_asc":
                    predbiljezbe = predbiljezbe.OrderBy(s => s.Statusi);
                    break;
                default:
                    break;
            }

            if (searchDate.HasValue)
            {
                predbiljezbe = predbiljezbe.Where(d => DbFunctions.TruncateTime(d.Datum) == searchDate);
            }

            if (!String.IsNullOrEmpty(predbiljezbaStatus))
            {
                switch (predbiljezbaStatus)
                {
                    case ("Prihvaceno"):
                        predbiljezbe = predbiljezbe.Where(x => x.Statusi == Status.Prihvacen);
                        break;
                    case ("Odbijeno"):
                        predbiljezbe = predbiljezbe.Where(x => x.Statusi == Status.Odbijen);
                        break;
                    case ("Neobradeno"):
                        predbiljezbe = predbiljezbe.Where(x => x.Statusi == Status.Neobrađeno);
                        break;
                    default:
                        break;
                }
            }
            return View("Predbiljezbe", predbiljezbe.ToList());
        }

        public ActionResult UrediPredbiljezbu(int id)
        {
            var predbiljezba = dbServis.DohvatiPredbiljezbe().FirstOrDefault(x => x.Id == id);
            if (predbiljezba == null)
            {
                return RedirectToAction("PopisPredbiljezbi");
            }
            return View(predbiljezba);
        }

        [HttpPost]
        public ActionResult UrediPredbiljezbu(PredbiljezbaModel predbiljezba)
        {
            dbServis.UrediPredbiljezbu(predbiljezba);
            return RedirectToAction("PopisPredbiljezbi");

        }

        #endregion


    }
}