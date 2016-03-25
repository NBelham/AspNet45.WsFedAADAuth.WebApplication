using System.Web;
using System.Web.Mvc;

namespace WsFedAADAuth.dotnet45.WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
