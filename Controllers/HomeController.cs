using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seminarski_rad_Olujic_AnaMaria.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("PopisPredbiljezbi", "Predbiljezba");
        }

        
    }
}