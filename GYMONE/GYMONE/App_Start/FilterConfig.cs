using System.Web;
using System.Web.Mvc;

namespace GYMONE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            // Apply  HandleErrorAttribute to all controller Action Method  //
            filters.Add(new HandleErrorAttribute());
        }
    }
}