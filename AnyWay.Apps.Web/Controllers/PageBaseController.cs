using AnyWay.Apps.Web.Attribute;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnyWay.Apps.Web.Controllers
{
    /// <summary>
    /// PageAuthorize:页面授权
    /// RequireHttps:强制使用Https
    /// </summary>
    [PageAuthorize]
    //[RequireHttps]
    public class PageBaseController : Controller
    {
        public SysUser GetSessionUser()
        {
            if (Session["AppUser"] == null)
            {
                return null;
            }

            return (Session["AppUser"] as SysSession).User;
        }

        public SysSession GetSession()
        {
            if (Session["AppUser"] == null)
            {
                return null;
            }
            return Session["AppUser"] as SysSession;
        }

        public bool SetSessionMenu(List<SysUserMenu> list)
        {
            if (Session["AppUser"] == null)
            {
                return false;
            }

            SysSession sysSession = Session["AppUser"] as SysSession;
            sysSession.UserMenu = list;
            Session["AppUser"] = sysSession;
            return true;
        }
    }
}