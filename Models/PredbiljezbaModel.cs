using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Seminarski_rad_Olujic_AnaMaria.Models
{
    public class PredbiljezbaModel
    {

        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Datum { get; set; }

        [Required(ErrorMessage = "Ime je obvezan podatak")]
        [StringLength(100)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obvezan podatak")]
        [StringLength(100)]
        public string Prezime { get; set; }

        [StringLength(100)]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Email je obvezan podatak")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email nije ispravan")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon je obvezan podatak")]
        public string Telefon { get; set; }

        public Status Statusi { get; set; }


        public virtual SeminarModel Seminar { get; set; }


    }
    public enum Status
    {
        Neobrađeno = 0,
        Prihvacen = 1,  
        Odbijen= 2
    }


}