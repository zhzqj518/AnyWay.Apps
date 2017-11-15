using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AnyWay.Apps.Web.Attribute
{
    public class PageHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            Exception Error = filterContext.Exception;
            string Message = Error.Message;//错误信息
            string Url = HttpContext.Current.Request.RawUrl;//错误发生地址

            filterContext.ExceptionHandled = true;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        ErrorType = this.GetType().Name,
                        ErrorUrl = Url,
                        Message = Message
                    }
                };
            }
            else
            {
                filterContext.Result = new RedirectResult(string.Format(ConfigurationManager.AppSettings["500ErrorUrl"], HttpUtility.UrlEncode(Error.Message), HttpUtility.UrlEncode(Url)));//跳转至错误提示页面
            }
        }
    }
}
