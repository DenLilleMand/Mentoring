using System.Web;
using System.Web.Mvc;

/**
 * Author: Jon
 */
namespace Mentor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
