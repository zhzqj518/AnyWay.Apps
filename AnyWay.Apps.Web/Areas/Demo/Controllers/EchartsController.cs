using AnyWay.Apps.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnyWay.Apps.Web.Areas.Demo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EchartsController : PageBaseController
    {
        // GET: Demo/Echarts
        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Bar()
        {
            return View();
        }

        public ActionResult Pie()
        {
            return View();
        }

        public ActionResult Radar()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Line()
        {
            return View();
        }

        public ActionResult Scatter()
        {
            return View();
        }
    }
}