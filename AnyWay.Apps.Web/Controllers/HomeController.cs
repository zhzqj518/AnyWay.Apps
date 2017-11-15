using AnyWay.Apps.Web.Attribute;
using ISystemBizManager;
using Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Unity.Attributes;

namespace AnyWay.Apps.Web.Controllers
{
    public class HomeController : PageBaseController
    {
        [Dependency]
        public ISysUserBizManager UserBizManager { get; set; }

        [Dependency]
        public ISysLogBizManager SysLogBizManager { get; set; }

        [PageNoAuthRole]
        public ActionResult Main()
        {
            ViewBag.UserName = GetSessionUser().UserName;
            return View();
        }

        [PageNoAuthRole]
        public ActionResult Index()
        {
            return View();
        }
    }
}