using AnyWay.Apps.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnyWay.Apps.Web.Areas.Demo.Controllers
{
    public class HighchartsController : PageBaseController
    {
        public ActionResult Line()
        {
            return View();
        }

        public ActionResult Area()
        {
            return View();
        }

        public ActionResult Bar()
        {
            return View();
        }
    }
}