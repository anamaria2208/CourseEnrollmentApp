using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Seminarski_rad_Olujic_AnaMaria.Models
{
    public class SeminarModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv seminara je obvezan podatak")]
        public string Naziv { get; set; }

        [DataType(DataType.MultilineText)]
        public string Opis { get; set; }

        [Required(ErrorMessage ="Datum seminara je obvezan podatak")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        public bool Popunjen { get; set; }

        [Required(ErrorMessage = "Predavac je obvezan podatak")]
        public string Predavac { get; set; }

        [Display(Name="Broj polaznika")]
        public virtual ICollection<PredbiljezbaModel> Predbiljezba { get; set; }

        

    }
}