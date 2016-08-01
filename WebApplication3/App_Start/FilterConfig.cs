using System.Web;
using System.Web.Mvc;
using WebApplication3.Filters;

namespace WebApplication3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute()); //Exception filter
            filters.Add(new EmployeeExceptionFilter());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
