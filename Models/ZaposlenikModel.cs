using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Seminarski_rad_Olujic_AnaMaria.Models
{
    public class ZaposlenikModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Ime zaposlenika je obvezan podatak")]
        [StringLength(100)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime zaposlenika je obvezan podatak")]
        [StringLength(100)]
        public string Prezime { get; set; }

        public string KorisnickoIme { get; set; }
        public string Password { get; set; }
    }
}