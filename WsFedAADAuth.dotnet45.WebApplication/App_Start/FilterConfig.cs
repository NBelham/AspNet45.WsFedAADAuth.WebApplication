using System.Web;
using System.Web.Mvc;

namespace dotnet45.WsFedAADAuth.WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
