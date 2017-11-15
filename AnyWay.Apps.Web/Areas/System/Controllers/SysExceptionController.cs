using AnyWay.Apps.Web.Controllers;
using ISystemBizManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;
using Models.System.ViewModel;
using Models.System;

namespace AnyWay.Apps.Web.Areas.System.Controllers
{
    public class SysExceptionController : PageBaseController
    {
        [Dependency]
        public ISysMenuSpclBizManager SysMenuSpclBizManager { get; set; }

        [AllowAnonymous]
        public ActionResult Exception500(string ErrorMsg, string ErrorUrl)
        {
            ErrorMsg = HttpUtility.UrlDecode(ErrorMsg);
            ErrorUrl = HttpUtility.UrlDecode(ErrorUrl);

            SysMenuSpclQuery menuSpclQuery = new SysMenuSpclQuery() { SortBy = "MenuNo", SortOrder = "ASC", OperateType = new List<string>() { "500" } };
            IList<SysMenuSpcl> menuSpclList = SysMenuSpclBizManager.GetList(menuSpclQuery);
            SysMenuSpcl menu = menuSpclList.Where(m => m.MenuIsValid == 1 && m.MenuIsLeaf == 1).FirstOrDefault() ?? new SysMenuSpcl();

            if (!string.IsNullOrEmpty(menu.MenuLink))
            {
                return Redirect(string.Format(menu.MenuLink, HttpUtility.UrlEncode(ErrorMsg), HttpUtility.UrlEncode(ErrorUrl)));
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Html500(string ErrorMsg, string ErrorUrl)
        {
            ErrorMsg = HttpUtility.UrlDecode(ErrorMsg);
            ErrorUrl = HttpUtility.UrlDecode(ErrorUrl);

            ViewBag.ErrorUrl = ErrorUrl;
            ViewBag.ErrorMsg = ErrorMsg;

            return View();
        }

        [AllowAnonymous]
        public ActionResult Exception404()
        {
            SysMenuSpclQuery menuSpclQuery = new SysMenuSpclQuery() { SortBy = "MenuNo", SortOrder = "ASC", OperateType = new List<string>() { "404" } };
            IList<SysMenuSpcl> menuSpclList = SysMenuSpclBizManager.GetList(menuSpclQuery);

            SysMenuSpcl menu = menuSpclList.Where(m => m.MenuIsValid == 1 && m.MenuIsLeaf == 1).FirstOrDefault() ?? new SysMenuSpcl();
            if (!string.IsNullOrEmpty(menu.MenuLink))
            {
                return Redirect(menu.MenuLink);
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Html404(string ErrorMsg, string ErrorUrl)
        {
            return View();
        }
    }
}