using System.Web;
using System.Web.Mvc;

namespace A2_HTML5_Biz_App_New
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
