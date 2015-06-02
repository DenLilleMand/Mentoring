using System.Web;
using System.Web.Mvc;

/**
 * Author: auto gen?
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
