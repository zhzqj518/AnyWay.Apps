using AnyWay.Apps.Web.Attribute;
using System.Web;
using System.Web.Mvc;

namespace AnyWay.Apps.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new PageHandleErrorAttribute());
        }
    }
}
