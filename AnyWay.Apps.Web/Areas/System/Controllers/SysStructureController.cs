using AnyWay.Apps.Core.Helper;
using AnyWay.Apps.Core.Message;
using AnyWay.Apps.Web.Controllers;
using ISystemBizManager;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace AnyWay.Apps.Web.Areas.System.Controllers
{
    /// <summary>
    /// 系统组织架构管理
    /// 1.用户
    /// 2.组织机构
    /// </summary>
    public class SysStructureController : PageBaseController
    {
        [Dependency]
        public ISysUserBizManager SysUserBizManager { set; get; }

        [Dependency]
        public ISysOrgBizManager SysOrgBizManager { set; get; }

        #region 系统管理员
        public ActionResult SysUserAdmin()
        {
            return View();
        }

        public JsonResult GetSysUserAdminList(string userName, int? pageIndex, int? pageSize, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "UserName";
            }

            SysUserQuery sysUserQuery = new SysUserQuery() { UserName = userName, UserType = new List<string>() { "superadmin" }, SortBy = sortField, SortOrder = sortOrder, PageIndex = pageIndex.Value, PageSize = pageSize.Value };
            TotalData userList = SysUserBizManager.GetTotalData(sysUserQuery);

            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        #endregion 系统管理员

        #region 用户管理
        public ActionResult SysUser()
        {
            return View();
        }

        public JsonResult GetSysUserList(string userName, int? pageIndex, int? pageSize, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "UserName";
            }

            SysUserQuery sysUserQuery = new SysUserQuery() { UserName = userName, UserType = new List<string>() { "common", "dev" }, SortBy = sortField, SortOrder = sortOrder, PageIndex = pageIndex.Value, PageSize = pageSize.Value };
            TotalData userList = SysUserBizManager.GetTotalData(sysUserQuery);

            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveUserChanges(string addData, string modifyData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                List<SysUser> addList = MiniUIHelper.Decode(addData, typeof(List<SysUser>)) as List<SysUser>;
                List<SysUser> modifyList = MiniUIHelper.Decode(modifyData, typeof(List<SysUser>)) as List<SysUser>;

                modifyList.ForEach(m =>
                {
                    m.UserUpdateBy = GetSessionUser().UserId;
                });

                result = SysUserBizManager.SaveUserChanges(addList, modifyList);

                if (result.isSuccess)
                {
                    result.enumErrorType = AnyWay.Apps.Core.Message.EnumErrorType.None;

                    #region 插入日志
                    string strIP = Request.ServerVariables["REMOTE_ADDR"];
                    List<SysLog> logList = new List<SysLog>();
                    if (addList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(addList),
                            Result = "Success",
                            Type = "Insert",
                            Module = "SysUser",
                            CreateTime = DateTime.Now
                        });
                    }
                    if (modifyList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(modifyList),
                            Result = "Success",
                            Type = "Update",
                            Module = "SysUser",
                            CreateTime = DateTime.Now
                        });
                    }
                    #endregion 插入日志
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
            }
            catch (Exception ex)
            {
                result = new ResultInfo() { isSuccess = false, DisplayErrorMsg = "程序在保存过程中出现错误，请重试！", enumErrorType = AnyWay.Apps.Core.Message.EnumErrorType.Exception };
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        #endregion 用户管理

        #region 组织机构管理
        public ActionResult SysOrg()
        {
            return View();
        }

        public JsonResult GetSysOrgList()
        {
            SysOrgQuery sysOrgQuery = new SysOrgQuery() { SortBy = "OrgNo", SortOrder = "ASC" };
            IList<SysOrg> orgList = SysOrgBizManager.GetList(sysOrgQuery);

            return Json(orgList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveOrgChanges(string addData, string modifyData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                List<SysOrg_MiniUI> addList_miniui = MiniUIHelper.Decode(addData, typeof(List<SysOrg_MiniUI>)) as List<SysOrg_MiniUI>;
                List<SysOrg> addList = new List<SysOrg>();
                List<SysOrg> modifyList = MiniUIHelper.Decode(modifyData, typeof(List<SysOrg>)) as List<SysOrg>;

                addList_miniui.ForEach(m =>
                {
                    m.OrgId = Guid.NewGuid().ToString("N");
                    addList_miniui.Where(n => n._pid == m._id).ToList().ForEach(o => { o.OrgParentId = m.OrgId; });
                    m.OrgCreateBy = GetSessionUser().UserId;
                    m.OrgCreateTime = DateTime.Now;
                    addList.Add(m);
                });

                modifyList.ForEach(m =>
                {
                    m.OrgUpdateBy = GetSessionUser().UserId;
                });

                result = SysOrgBizManager.SaveOrgChanges(addList, modifyList);

                if (result.isSuccess)
                {
                    result.enumErrorType = AnyWay.Apps.Core.Message.EnumErrorType.None;

                    #region 插入日志
                    string strIP = Request.ServerVariables["REMOTE_ADDR"];
                    List<SysLog> logList = new List<SysLog>();
                    if (addList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(addList),
                            Result = "Success",
                            Type = "Insert",
                            Module = "SysOrg",
                            CreateTime = DateTime.Now
                        });
                    }
                    if (modifyList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(modifyList),
                            Result = "Success",
                            Type = "Update",
                            Module = "SysOrg",
                            CreateTime = DateTime.Now
                        });
                    }
                    #endregion 插入日志
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
            }
            catch (Exception ex)
            {
                result = new ResultInfo() { isSuccess = false, DisplayErrorMsg = "程序在保存过程中出现错误，请重试！", enumErrorType = AnyWay.Apps.Core.Message.EnumErrorType.Exception };
            }
            return Json(result, JsonRequestBehavior.DenyGet);
        }

        #endregion 组织机构管理
    }
}