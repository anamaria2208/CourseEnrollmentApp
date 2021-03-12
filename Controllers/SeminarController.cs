using Seminarski_rad_Olujic_AnaMaria.Models;
using Seminarski_rad_Olujic_AnaMaria.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Seminarski_rad_Olujic_AnaMaria.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        #region General- instanciranje DbService class

        private readonly DbService dbServis;
        public SeminarController()
        {
            this.dbServis = new DbService();
        }
        #endregion



        #region Seminari

        //get: popis seminara za kreiranje predbiljezbe
        //search: naziv i opis
        //filter: datum
        //sort: naziv, opis, datum, predavac
        [AllowAnonymous]
        public ActionResult PopisSeminaraZaPredbiljezbu(string searchString, DateTime? searchDate, string sortOrder)
        {
      

            var seminari = dbServis.DohvatiSeminare().Where(x => x.Popunjen == false);

            ViewBag.SortNaziv = sortOrder == "naziv_asc" ? "naziv_desc" : "naziv_asc";
            ViewBag.SortDatum = sortOrder == "datum_asc" ? "datum_desc" : "datum_asc";
            ViewBag.SortOpis = sortOrder == "opis_asc" ? "opis_desc" : "opis_asc";
            ViewBag.SortPredavac = sortOrder == "predavac_asc" ? "predavac_desc" : "predavac_asc";

            switch (sortOrder)
            {
                case "naziv_desc":
                    seminari = seminari.OrderByDescending(s => s.Naziv);
                    break;
                case "naziv_asc":
                    seminari = seminari.OrderBy(s => s.Naziv);
                    break;
                case "datum_desc":
                    seminari = seminari.OrderByDescending(x => x.Datum);
                    break;
                case "datum_asc":
                    seminari = seminari.OrderBy(s => s.Datum);
                    break;
                case "opis_desc":
                    seminari = seminari.OrderByDescending(x => x.Opis);
                    break;
                case "opis_asc":
                    seminari = seminari.OrderBy(s => s.Opis);
                    break;
                case "predavac_desc":
                    seminari = seminari.OrderByDescending(x => x.Predavac);
                    break;
                case "predavac_asc":
                    seminari = seminari.OrderBy(s => s.Predavac);
                    break;
                default:
                    break;
            }

            
            if (!String.IsNullOrEmpty(searchString))
            {
                seminari = seminari.Where(n => n.Naziv.Contains(searchString)
                           || n.Opis.Contains(searchString));
            }

            if (searchDate.HasValue)
            {
                ViewBag.datum = searchDate;
                seminari = seminari.Where(d => d.Datum == searchDate);
            }

            return View("Predbiljezba", seminari);
        }


        //get: popis seminara za administriranje
        //search: naziv i opis
        //filter: datum
        //sort: naziv, datum, opis, broj polaznika, predavac
        public ActionResult PopisSeminara(string searchString, string sortOrder, DateTime? searchDate)
        {
            ViewBag.SortNaziv = sortOrder == "naziv_asc" ? "naziv_desc" : "naziv_asc";
            ViewBag.SortDatum = sortOrder == "datum_asc" ? "datum_desc" : "datum_asc";
            ViewBag.SortOpis = sortOrder == "opis_asc" ? "opis_desc" : "opis_asc";
            ViewBag.SortBrojPolaznika = sortOrder == "brPolaznika_asc" ? "brPolaznika_desc" : "brPolaznika_asc";
            ViewBag.SortPredavac = sortOrder == "predavac_asc" ? "predavac_desc" : "predavac_asc";

            var seminari = dbServis.DohvatiSeminare();
            if (!String.IsNullOrEmpty(searchString))
            {
                seminari = seminari.Where(n => n.Naziv.Contains(searchString)
                || n.Opis.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "naziv_desc":
                    seminari = seminari.OrderByDescending(s => s.Naziv);
                    break;
                case "naziv_asc":
                    seminari = seminari.OrderBy(s => s.Naziv);
                    break;
                case "datum_desc":
                    seminari = seminari.OrderByDescending(x => x.Datum);
                    break;
                case "datum_asc":
                    seminari = seminari.OrderBy(s => s.Datum);
                    break;
                case "opis_desc":
                    seminari = seminari.OrderByDescending(x => x.Opis);
                    break;
                case "opis_asc":
                    seminari = seminari.OrderBy(s => s.Opis);
                    break;
                case "brPolaznika_desc":
                    seminari = seminari.OrderByDescending(x => x.Predbiljezba.Count);
                    break;
                case "brPolaznika_asc":
                    seminari = seminari.OrderBy(s => s.Predbiljezba.Count);
                    break;
                case "predavac_desc":
                    seminari = seminari.OrderByDescending(x => x.Predavac);
                    break;
                case "predavac_asc":
                    seminari = seminari.OrderBy(s => s.Predavac);
                    break;
                default:
                    break;
            }

            if (searchDate.HasValue)
            {
                ViewBag.datum = searchDate;
                seminari = seminari.Where(d => d.Datum == searchDate);
            }

            return View("Seminari", seminari.ToList());
        }

        public ActionResult KreirajSeminar()
        {
            var seminar = new SeminarModel()
            {
                Datum = DateTime.Now
            };
            return View(seminar);
        }

        [HttpPost]
        public ActionResult KreirajSeminar(SeminarModel seminar)
        {
            dbServis.KreirajSeminar(seminar);
            return RedirectToAction("PopisSeminara");
        }

        public ActionResult UrediSeminar(int id)
        {
            var seminar = dbServis.DohvatiSeminare().FirstOrDefault(x => x.Id == id);
            if (seminar == null)
            {
                return RedirectToAction("PopisSeminara");
            }

            return View(seminar);
        }

        [HttpPost]
        public ActionResult UrediSeminar(SeminarModel seminar)
        {
            dbServis.UrediSeminar(seminar);
            return RedirectToAction("PopisSeminara");
        }

        public ActionResult ObrisiSeminar(int id)
        {
            var seminar = dbServis.DohvatiSeminare().FirstOrDefault(x => x.Id == id);
            if (seminar == null)
            {
                return RedirectToAction("PopisSeminara");
            }
            if (seminar.Predbiljezba.Count > 0)
            {
                return View("ObrisiSeminar");
             
            }
            else
            {
                dbServis.ObrisiSeminar(seminar);
            }
            
            return RedirectToAction("PopisSeminara");
        }
        #endregion
    }
}