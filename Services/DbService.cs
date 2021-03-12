using Seminarski_rad_Olujic_AnaMaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Seminarski_rad_Olujic_AnaMaria.Services
{

    //probati automapere
    public class DbService
    {

        #region General
        private ApplicationDbContext db;

        public DbService()
        {
            this.db = new ApplicationDbContext();
        }
        #endregion


        #region SeminarDb
        //dohvati sve seminare, ne radi query nad bazom jos
        public IQueryable<SeminarModel> DohvatiSeminare()
        {
            var seminari = from s in db.Seminar select s;
            return seminari;
        }

        public void KreirajSeminar(SeminarModel seminar)
        {
            db.Seminar.Add(seminar);
            db.SaveChanges();
        }

        public void UrediSeminar(SeminarModel seminar)
        {
            var seminarObj = db.Seminar.FirstOrDefault(x => x.Id == seminar.Id);
            //TO DO: probati sa automaperom
            seminarObj.Naziv = seminar.Naziv;
            seminarObj.Opis = seminar.Opis;
            seminarObj.Popunjen = seminar.Popunjen;
            seminarObj.Predavac = seminar.Predavac;
            seminarObj.Datum = seminar.Datum;
            db.SaveChanges();
        }

        public void ObrisiSeminar(SeminarModel seminarObj)
        {
            db.Seminar.Remove(seminarObj);
            db.SaveChanges();
        }

        #endregion


        #region PredbiljezbaDb
        public IQueryable<PredbiljezbaModel> DohvatiPredbiljezbe()
        {
            var predbiljezbe = from p in db.Predbiljezba select p;
            return predbiljezbe;
        }

        public void KreirajPredbiljezbu(PredbiljezbaModel predbiljezba)
        {
            db.Predbiljezba.Add(predbiljezba);
            db.SaveChanges();
        }

        public void UrediPredbiljezbu(PredbiljezbaModel predbiljezba)
        {
            var prebiljezbaObj = db.Predbiljezba.FirstOrDefault(x => x.Id == predbiljezba.Id);
            //TO DO: probati sa automaperom
            prebiljezbaObj.Adresa = predbiljezba.Adresa;
            prebiljezbaObj.Datum = predbiljezba.Datum;
            prebiljezbaObj.Email = predbiljezba.Email;
            prebiljezbaObj.Ime = predbiljezba.Ime;
            prebiljezbaObj.Prezime = predbiljezba.Prezime;
            prebiljezbaObj.Statusi = predbiljezba.Statusi;
            prebiljezbaObj.Telefon = predbiljezba.Telefon;
            db.SaveChanges();
        }

        #endregion
    }
}