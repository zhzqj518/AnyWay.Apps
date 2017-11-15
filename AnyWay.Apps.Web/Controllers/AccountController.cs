using AnyWay.Apps.Core.Helper;
using AnyWay.Apps.Core.Message;
using AnyWay.Apps.Web.Models;
using ISystemBizManager;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace AnyWay.Apps.Web.Controllers
{
    public class AccountController : PageBaseController
    {
        [Dependency]
        public ISysUserBizManager SysUserBizManager { set; get; }

        [Dependency]
        public ISysMenuBizManager SysMenuBizManager { set; get; }

        [Dependency]
        public ISysMenuActionBizManager SysMenuActionBizManager { set; get; }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (Session["AppUser"] != null)
            {
                return RedirectToAction("Main", "Home");
            }
            return View();
        }

        public ActionResult ModifyPwd()
        {
            return View();
        }

        public ActionResult ModifyInfo()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetValidateCode()
        {
            string validateCode = StringHelper.CreateValidateCode(5);
            Session["AppValidateCode"] = validateCode;
            byte[] bytes = StringHelper.CreateValidateGraphic(validateCode);
            return File(bytes, @"image/jpeg");
        }

        [AllowAnonymous]
        public JsonResult UserLogin(UserLoginModel userLoginModel)
        {
            if (userLoginModel.VerificateCode != Session["AppValidateCode"].ToString())
            {
                return Json(new LoginResultJson()
                {
                    IsSuccess = false,
                    UserTicketID = string.Empty,
                    ErrorMsg = "验证码不正确！"
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginResult result = SysUserBizManager.UserLogin(userLoginModel);

            LoginResultJson loginResultJson = new LoginResultJson();
            loginResultJson.IsSuccess = result.IsSuccess;
            loginResultJson.UserTicketID = result.User.UserTicketID;
            loginResultJson.ErrorMsg = result.ErrorMsg;

            //登录成功，赋值Session
            if (result.IsSuccess)
            {
                SysSession sysSession = new SysSession();
                sysSession.User = result.User;

                Session["AppUser"] = sysSession;
            }

            return Json(loginResultJson, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserLogout()
        {
            Session.Clear();

            return Json(new
            {
                isSuccess = true
            }, JsonRequestBehavior.DenyGet);
        }

        public JsonResult UserModifyPwd(string old_pwd, string new_pwd)
        {
            if (GetSessionUser() == null)
            {
                return Json(new { isSuccess = false, errorMsg = "会话已过期，请重新登录修改密码！" }, JsonRequestBehavior.AllowGet);
            }

            if (GetSessionUser().UserPwd != old_pwd)
            {
                return Json(new { isSuccess = false, errorMsg = "旧密码不正确，请重新修改密码！" }, JsonRequestBehavior.AllowGet);
            }

            SysUser user = GetSessionUser();
            user.UserPwd = new_pwd;
            List<SysUser> update = new List<SysUser>();
            update.Add(user);

            ResultInfo result = SysUserBizManager.SaveUserChanges(new List<SysUser>(), update);

            if (result.isSuccess)
            {
                result.enumErrorType = AnyWay.Apps.Core.Message.EnumErrorType.None;
                string strIP = Request.ServerVariables["REMOTE_ADDR"];
                List<SysLog> logList = new List<SysLog>();
                logList.Add(new SysLog()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Operator = GetSessionUser().UserId,
                    OperatorIP = strIP,
                    Message = MiniUIHelper.Encode(update),
                    Result = "Success",
                    Type = "Update",
                    Module = "SysUserModifyPwd",
                    CreateTime = DateTime.Now
                });

                Session.Clear();
            }
            else
            {
                result.enumErrorType = AnyWay.Apps.Core.Message.EnumErrorType.DBSave;
                result.DisplayErrorMsg = string.Empty;
                foreach (string item in result.ErrorMsgList)
                {
                    result.DisplayErrorMsg = string.Concat(result.DisplayErrorMsg, item, "<br/>");
                }
            }
            return Json(new { isSuccess = result.isSuccess, errorMsg = result.DisplayErrorMsg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserActions(string menuId, string menuLink)
        {
            List<SysMenuActionLink> actionList = new List<SysMenuActionLink>();
            SysMenuActionQuery sysMenuActionQuery = new SysMenuActionQuery();
            sysMenuActionQuery = new SysMenuActionQuery { UserId = GetSessionUser().UserId, MenuId = menuId, MenuLink = menuLink.ToLower() };
            actionList = SysMenuActionBizManager.GetMenuRoleActions(sysMenuActionQuery).ToList();

            bool isHaveActions = actionList.Count > 0 ? true : false;
            //List<SysMenuActionLink> roleActionList = actionList.Where(m => m.ActionIsCheck == 1 && m.ActionIsValid == 1).ToList();

            return Json(new
            {
                isHaveActions = isHaveActions,
                roleActionList = actionList
            }, JsonRequestBehavior.AllowGet);
        }
    }
}