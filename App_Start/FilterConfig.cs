using System.Web;
using System.Web.Mvc;

namespace Seminarski_rad_Olujic_AnaMaria
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
