using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.System;
using Models.System.ViewModel;
using AnyWay.Apps.Core.Helper;

namespace AnyWay.Apps.Web.Attribute
{
    public class PageAuthorizeAttribute : AuthorizeAttribute
    {
        private System.Net.HttpStatusCode _HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
        private EnumErrorType _EnumErrorType = EnumErrorType.PermissionDenied;
        private string _ErrorMsg = string.Empty;

        /// <summary>
        /// 权限检查
        /// AllowAnonymous 标签自动过滤
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //配合AllowAnonymous特性
            bool isSkip = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                 || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
            if (isSkip)
            {
                return;
            }

            string actionName = filterContext.ActionDescriptor.ActionName;
            if (!ValidSession(filterContext.HttpContext))
            {
                _HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
                _EnumErrorType = EnumErrorType.SessionTimeout;
                _ErrorMsg = "您未登录或者会话已结束";
                HandleUnauthorizedRequest(filterContext);
            }
            else if (!ValidRolePage(filterContext))
            {
                _HttpStatusCode = System.Net.HttpStatusCode.Forbidden;
                _EnumErrorType = EnumErrorType.PermissionDenied;
                _ErrorMsg = "您没有权限访问该方法或者页面";
                HandleUnauthorizedRequest(filterContext);
            }
        }

        /// <summary>  
        /// 处理授权失败的 HTTP 请求。  
        /// </summary>  
        /// <param name="filterContext">封装用于 System.Web.Mvc.AuthorizeAttribute 的信息。 filterContext 对象包括控制器、HTTP 上下文、请求上下文、操作结果和路由数据。</param>  
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //异步请求  
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = (int)_HttpStatusCode;
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        ErrorType = this.GetType().Name,
                        Action = filterContext.ActionDescriptor.ActionName,
                        Message = _ErrorMsg
                    }
                };
            }
            else
            {
                string MyAuthError = ConfigurationManager.AppSettings["AuthErrorUrl"];
                if (_EnumErrorType == EnumErrorType.SessionTimeout)
                {
                    MyAuthError = ConfigurationManager.AppSettings["SessionTimeoutUrl"];
                }
                filterContext.Result = new RedirectResult(MyAuthError);
            }
        }

        private bool ValidSession(HttpContextBase httpContext)
        {
            if (httpContext.Session["AppUser"] == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 只做到了验证页面的功能，还没做到验证方法的功能
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool ValidRolePage(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                bool isSkip = filterContext.ActionDescriptor.IsDefined(typeof(PageNoAuthRoleAttribute), inherit: true)
                     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(PageNoAuthRoleAttribute), inherit: true);

                //不需要验证权限属性，直接跳过
                //例子：页面/Home/Index
                //例子：用户类型是超级管理员
                if (isSkip)
                {
                    return true;
                }

                SysSession sysSession = filterContext.HttpContext.Session["AppUser"] as SysSession;
                SysUser sysUser = sysSession.User;
                if (sysUser.UserType == "superadmin")
                {
                    return true;
                }

                var controllerName = (filterContext.RouteData.Values["controller"]).ToString().ToLower();
                var actionName = (filterContext.RouteData.Values["action"]).ToString().ToLower();
                var areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString().ToLower();

                //验证有没有页面权限
                //我把用户网页权限放在了Session里面，Session存在ASP.Net State进程里面
                //为什么存在Session里面，因为AuthorizeAttribute里面不能使用[Dependency]特性，也就是说不支持依赖注入，原因在于AuthorizeAttribute不经过Factory创建，也就不经过托管
                string menuLink = areaName == "" ? (controllerName + "/" + actionName) : (areaName + "/" + controllerName + "/" + actionName);
                bool isFlag = false;
                foreach (SysUserMenu menu in sysSession.UserMenu)
                {
                    if (string.IsNullOrEmpty(menu.MenuLink))
                    {
                        continue;
                    }
                    string strMenuLink = menu.MenuLink;
                    if (strMenuLink.IndexOf("?") > -1)
                    {
                        strMenuLink = strMenuLink.Substring(0, strMenuLink.IndexOf("?"));
                    }
                    if (strMenuLink.EndsWith(menuLink, StringComparison.CurrentCultureIgnoreCase))
                    {
                        isFlag = true;
                        break;
                    }
                }

                return isFlag;
            }

            return true;
        }
    }

    public enum EnumErrorType
    {
        SessionTimeout = 10001,
        PermissionDenied = 10002
    }
}
