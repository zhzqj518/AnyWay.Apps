using AnyWay.Apps.Core.Helper;
using AnyWay.Apps.Core.Message;
using AnyWay.Apps.Web.Areas.System.Models;
using AnyWay.Apps.Web.Controllers;
using ISystemBizManager;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Unity.Attributes;

namespace AnyWay.Apps.Web.Areas.System.Controllers
{
    /// <summary>
    /// 系统权限管理
    /// 1.菜单
    /// 2.功能用户组
    /// 3.数据用户组
    /// </summary>
    public class SysAuthorityController : PageBaseController
    {
        #region Unity IoC
        [Dependency]
        public ISysMenuBizManager SysMenuBizManager { get; set; }

        [Dependency]
        public ISysMenuActionBizManager SysMenuActionBizManager { get; set; }

        [Dependency]
        public ISysLogBizManager SysLogBizManager { get; set; }

        [Dependency]
        public ISysGroupBizManager SysGroupBizManager { get; set; }

        [Dependency]
        public ISysGroupMenuBizManager SysGroupMenuBizManager { get; set; }

        [Dependency]
        public ISysGroupUserBizManager SysGroupUserBizManager { get; set; }

        [Dependency]
        public ISysGroupOrgBizManager SysGroupOrgBizManager { get; set; }

        [Dependency]
        public ISysGroupMenuActionBizManager SysGroupMenuActionBizManager { get; set; }

        [Dependency]
        public ISysMenuSpclBizManager SysMenuSpclBizManager { get; set; }
        #endregion Unity IoC

        #region 菜单管理
        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult SysMenu()
        {
            return View();
        }

        public JsonResult GetSysRoleMenu()
        {
            SysInitModel initModel = new SysInitModel();
            SysIndexJsonModel indexModel = new SysIndexJsonModel();
            List<SysMenuJsonModel> menuList = new List<SysMenuJsonModel>();

            SysMenuQuery menuQuery = new SysMenuQuery() { UserId = GetSessionUser().UserId, UserType = GetSessionUser().UserType, SortBy = "MenuNo", SortOrder = "ASC", OperateType = new List<string>() { "01", "02" } };
            List<SysMenu> userMenuList = SysMenuBizManager.GetUserList(menuQuery).ToList();

            SysMenuSpclQuery menuSpclQuery = new SysMenuSpclQuery() { SortBy = "MenuNo", SortOrder = "ASC", OperateType = new List<string>() { "index" } };
            IList<SysMenuSpcl> menuSpclList = SysMenuSpclBizManager.GetList(menuSpclQuery);

            IList<SysMenu> topMenu = new List<SysMenu>();//userMenuList.Where(m => m.MenuType == "top").ToList();
            List<SysUserMenu> sysUserMenu = new List<SysUserMenu>();
            userMenuList.ForEach(m =>
            {
                if (m.MenuType == "top")
                {
                    topMenu.Add(m);
                }
                sysUserMenu.Add(new SysUserMenu() { MenuId = m.MenuId, MenuLink = m.MenuLink });
            });
            base.SetSessionMenu(sysUserMenu);

            SysMenuSpcl indexMenu = menuSpclList.Where(m => m.MenuIsValid == 1 && m.MenuIsLeaf == 1).FirstOrDefault() ?? new SysMenuSpcl();

            for (int i = 0; i < topMenu.Count; i++)
            {
                #region 构造
                SysMenu top = topMenu[i];
                SysMenuJsonModel topModel = new SysMenuJsonModel();
                topModel.text = top.MenuName;
                topModel.icon = top.MenuIcon;
                topModel.iconCls = top.MenuIconPath;
                topModel.href = top.MenuLink;
                topModel.isChecked = false;
                if (i == 0)
                {
                    topModel.isChecked = true;
                }
                topModel.children = new List<SysMenuSlider>();
                IList<SysMenu> sildeMenu = userMenuList.Where(m => m.MenuParentId == top.MenuId).OrderBy(m => m.MenuNo).ToList();
                for (int j = 0; j < sildeMenu.Count; j++)
                {
                    SysMenu silde = sildeMenu[j];
                    SysMenuSlider sildeModel = new SysMenuSlider();
                    sildeModel.id = silde.MenuId;
                    sildeModel.pid = silde.MenuParentId;
                    sildeModel.text = silde.MenuName;
                    sildeModel.icon = silde.MenuIcon;
                    sildeModel.iconCls = silde.MenuIconPath;
                    sildeModel.href = silde.MenuLink;
                    sildeModel.isChecked = false;
                    if (j == 0)
                    {
                        sildeModel.isChecked = true;
                    }
                    sildeModel.children = new List<SysMenuTree>();
                    GetRecursiveMenu(sildeModel.id, userMenuList, sildeModel.children);
                    if (sildeModel.children.Count > 0)
                    {
                        sildeModel.children = sildeModel.children.OrderBy(m => m.sort).ToList();
                    }
                    topModel.children.Add(sildeModel);
                }
                menuList.Add(topModel);
                #endregion 构造
            }

            initModel.indexJson = new SysIndexJsonModel() { id = indexMenu.MenuId, text = indexMenu.MenuName, icon = indexMenu.MenuIcon, iconCls = indexMenu.MenuIconPath, href = indexMenu.MenuLink };
            initModel.menuJson = menuList;

            return Json(initModel, JsonRequestBehavior.AllowGet);
        }

        private void GetRecursiveMenu(string pid, IList<SysMenu> menu, List<SysMenuTree> list)
        {
            List<SysMenu> child = menu.Where(m => m.MenuParentId == pid && m.MenuIsHidden == 0).ToList();

            foreach (SysMenu item in child)
            {
                list.Add(new SysMenuTree() { id = item.MenuId, pid = item.MenuParentId, text = item.MenuName, icon = item.MenuIcon, iconCls = item.MenuIconPath, href = item.MenuLink, sort = item.MenuNo });
                if (menu.Where(m => m.MenuParentId == item.MenuId).Count() > 0)
                {
                    GetRecursiveMenu(item.MenuId, menu, list);
                }
            }
        }

        public JsonResult GetSysAllMenu()
        {
            SysMenuQuery menuQuery = new SysMenuQuery() { SortBy = "MenuNo", SortOrder = "ASC" };
            IList<SysMenu> menuList = SysMenuBizManager.GetList(menuQuery);

            return Json(menuList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存菜单
        /// addData:新增数据
        /// modifyData:修改数据
        /// removeData:删除数据
        /// 本方法只支持Post方式
        /// </summary>
        [HttpPost]
        public JsonResult SaveMenuChanges(string addData, string modifyData, string removeData, string addActionData, string modifyActionData, string removeActionData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                //将新增数转换为miniui格式
                List<SysMenu_MiniUI> addList_miniui = MiniUIHelper.Decode(addData, typeof(List<SysMenu_MiniUI>)) as List<SysMenu_MiniUI>;
                List<SysMenu> addList = new List<SysMenu>();
                List<SysMenu> modifyList = MiniUIHelper.Decode(modifyData, typeof(List<SysMenu>)) as List<SysMenu>;
                List<SysMenu> removeList = MiniUIHelper.Decode(removeData, typeof(List<SysMenu>)) as List<SysMenu>;

                List<SysMenuAction_MiniUI> addActionList_miniui = MiniUIHelper.Decode(addActionData, typeof(List<SysMenuAction_MiniUI>)) as List<SysMenuAction_MiniUI>;
                List<SysMenuAction> addActionList = new List<SysMenuAction>();
                List<SysMenuAction> modifyActionList = MiniUIHelper.Decode(modifyActionData, typeof(List<SysMenuAction>)) as List<SysMenuAction>;
                List<SysMenuAction> removeActionList = MiniUIHelper.Decode(removeActionData, typeof(List<SysMenuAction>)) as List<SysMenuAction>;

                addList_miniui.ForEach(m =>
                {
                    m.MenuId = Guid.NewGuid().ToString("N");
                    if (string.IsNullOrEmpty(m.MenuType))
                    {
                        switch (m._level)
                        {
                            case "0":
                                {
                                    m.MenuType = "top";
                                    break;
                                }
                            case "1":
                                {
                                    m.MenuType = "slide";
                                    break;
                                }
                            default:
                                {
                                    m.MenuType = "tree";
                                    break;
                                }
                        }
                    }

                    addList_miniui.Where(n => n._pid == m._id).ToList().ForEach(o => { o.MenuParentId = m.MenuId; });
                    m.MenuCreateBy = GetSessionUser().UserId;
                    m.MenuCreateTime = DateTime.Now;
                    addList.Add(m);
                });

                modifyList.ForEach(m =>
                {
                    m.MenuUpdateBy = GetSessionUser().UserId;
                });

                addActionList_miniui.ForEach(m =>
                {
                    m.ActionId = Guid.NewGuid().ToString("N");
                    SysMenu_MiniUI obj = addList_miniui.Where(n => n._id == m._menuid).FirstOrDefault();
                    if (obj != null)//新增的菜单
                    {
                        m.MenuId = obj.MenuId;
                    }
                    m.ActionCreateBy = GetSessionUser().UserId;
                    m.ActionCreateTime = DateTime.Now;
                    addActionList.Add(m);
                });

                modifyActionList.ForEach(m =>
                {
                    m.ActionUpdateBy = GetSessionUser().UserId;
                });

                result = SysMenuBizManager.SaveMenuChanges(addList, modifyList, removeList, addActionList, modifyActionList, removeActionList);


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
                            Module = "SysMenu",
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
                            Module = "SysMenu",
                            CreateTime = DateTime.Now
                        });
                    }
                    if (removeList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(removeList),
                            Result = "Success",
                            Type = "Delete",
                            Module = "SysMenu",
                            CreateTime = DateTime.Now
                        });
                    }
                    if (logList.Count > 0)
                    {
                        SysLogBizManager.Insert(logList);
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

        public JsonResult GetSysMenuActions(string menuId, int? pageIndex, int? pageSize, string sortField, string sortOrder)
        {
            SysMenuActionQuery menuActionQuery = new SysMenuActionQuery() { MenuId = menuId, SortBy = sortField, SortOrder = sortOrder, PageIndex = pageIndex.Value, PageSize = pageSize.Value };
            TotalData totalData = SysMenuActionBizManager.GetTotalData(menuActionQuery);

            return Json(totalData, JsonRequestBehavior.AllowGet);
        }

        #endregion 菜单管理

        #region 特殊菜单
        public ActionResult SysMenuSpcl()
        {
            return View();
        }

        public JsonResult GetSysAllMenuSpcl()
        {
            SysMenuSpclQuery menuSpclQuery = new SysMenuSpclQuery() { SortBy = "MenuNo", SortOrder = "ASC" };
            IList<SysMenuSpcl> menuSpclList = SysMenuSpclBizManager.GetList(menuSpclQuery);
            return Json(menuSpclList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveMenuSpclChanges(string addData, string modifyData, string removeData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                //将新增数转换为miniui格式
                List<SysMenuSpcl_MiniUI> addList_miniui = MiniUIHelper.Decode(addData, typeof(List<SysMenuSpcl_MiniUI>)) as List<SysMenuSpcl_MiniUI>;
                List<SysMenuSpcl> addList = new List<SysMenuSpcl>();
                List<SysMenuSpcl> modifyList = MiniUIHelper.Decode(modifyData, typeof(List<SysMenuSpcl>)) as List<SysMenuSpcl>;
                List<SysMenuSpcl> removeList = MiniUIHelper.Decode(removeData, typeof(List<SysMenuSpcl>)) as List<SysMenuSpcl>;

                addList_miniui.ForEach(m =>
                {
                    m.MenuId = Guid.NewGuid().ToString("N");
                    addList_miniui.Where(n => n._pid == m._id).ToList().ForEach(o => { o.MenuParentId = m.MenuId; });
                    m.MenuCreateBy = GetSessionUser().UserId;
                    m.MenuCreateTime = DateTime.Now;
                    addList.Add(m);
                });

                modifyList.ForEach(m =>
                {
                    m.MenuUpdateBy = GetSessionUser().UserId;
                });

                result = SysMenuSpclBizManager.SaveMenuSpclChanges(addList, modifyList, removeList);

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
                            Module = "SysMenuSpcl",
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
                            Module = "SysMenuSpcl",
                            CreateTime = DateTime.Now
                        });
                    }

                    if (removeList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(removeList),
                            Result = "Success",
                            Type = "Delete",
                            Module = "SysMenuSpcl",
                            CreateTime = DateTime.Now
                        });
                    }
                    if (logList.Count > 0)
                    {
                        SysLogBizManager.Insert(logList);
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

        #endregion 特殊菜单

        #region 用户组管理
        public ActionResult SysFuncGroup()
        {
            return View();
        }

        public ActionResult SysDataGroup()
        {
            return View();
        }

        public ActionResult AssignFuncUser(string groupId)
        {
            ViewBag.groupId = groupId;
            return View();
        }

        public ActionResult AssignFuncMenu(string groupId)
        {
            ViewBag.groupId = groupId;
            return View();
        }

        public ActionResult AssignDataUser(string groupId)
        {
            ViewBag.groupId = groupId;
            return View();
        }

        public ActionResult AssignDataOrg(string groupId)
        {
            ViewBag.groupId = groupId;
            return View();
        }

        public JsonResult GetSysFuncGroupList(string groupName, int? pageIndex, int? pageSize, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "GroupName";
            }
            SysGroupQuery groupQuery = new SysGroupQuery() { GroupType = "func", GroupName = groupName, SortBy = sortField, SortOrder = sortOrder, PageIndex = pageIndex.Value, PageSize = pageSize.Value };
            TotalData groupList = SysGroupBizManager.GetTotalData(groupQuery);

            return Json(groupList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSysDataGroupList(string groupName, int? pageIndex, int? pageSize, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "GroupName";
            }
            SysGroupQuery groupQuery = new SysGroupQuery() { GroupType = "data", GroupName = groupName, SortBy = sortField, SortOrder = sortOrder, PageIndex = pageIndex.Value, PageSize = pageSize.Value };

            TotalData groupList = SysGroupBizManager.GetTotalData(groupQuery);

            return Json(groupList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveGroupChanges(string addData, string modifyData, string removeData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                List<SysGroup> addList = MiniUIHelper.Decode(addData, typeof(List<SysGroup>)) as List<SysGroup>;
                List<SysGroup> modifyList = MiniUIHelper.Decode(modifyData, typeof(List<SysGroup>)) as List<SysGroup>;
                List<SysGroup> removeList = MiniUIHelper.Decode(removeData, typeof(List<SysGroup>)) as List<SysGroup>;

                result = SysGroupBizManager.SaveGroupChanges(addList, modifyList, removeList);

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
                            Module = "SysGroup",
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
                            Module = "SysGroup",
                            CreateTime = DateTime.Now
                        });
                    }
                    if (removeList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(removeList),
                            Result = "Success",
                            Type = "Delete",
                            Module = "SysGroup",
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

        public JsonResult GetUnAssignSysUserList(string groupId, string userName, int? pageIndex, int? pageSize, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "UserName";
            }

            SysGroupUserQuery sysUserQuery = new SysGroupUserQuery() { GroupId = groupId, UserName = userName, SortBy = sortField, SortOrder = sortOrder, PageIndex = pageIndex.Value, PageSize = pageSize.Value };
            TotalData userList = SysGroupUserBizManager.GetUnAssignSysUserTotalData(sysUserQuery);

            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssignSysUserList(string groupId, string userName, int? pageIndex, int? pageSize, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "UserName";
            }

            SysGroupUserQuery sysUserQuery = new SysGroupUserQuery() { GroupId = groupId, UserName = userName, SortBy = sortField, SortOrder = sortOrder, PageIndex = pageIndex.Value, PageSize = pageSize.Value };
            TotalData userList = SysGroupUserBizManager.GetAssignSysUserTotalData(sysUserQuery);

            return Json(userList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAssignGroupUser(string addData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                List<SysGroupUser> addList = MiniUIHelper.Decode(addData, typeof(List<SysGroupUser>)) as List<SysGroupUser>;

                result = SysGroupUserBizManager.SaveAssignGroupUser(addList);

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
                            Module = "SysGroupUser",
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

        [HttpPost]
        public JsonResult SaveUnAssignGroupUser(string removeData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                List<SysGroupUser> removeList = MiniUIHelper.Decode(removeData, typeof(List<SysGroupUser>)) as List<SysGroupUser>;

                result = SysGroupUserBizManager.SaveUnAssignGroupUser(removeList);

                if (result.isSuccess)
                {
                    result.enumErrorType = AnyWay.Apps.Core.Message.EnumErrorType.None;

                    #region 插入日志
                    string strIP = Request.ServerVariables["REMOTE_ADDR"];
                    List<SysLog> logList = new List<SysLog>();
                    if (removeList.Count > 0)
                    {
                        logList.Add(new SysLog()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            Operator = GetSessionUser().UserId,
                            OperatorIP = strIP,
                            Message = MiniUIHelper.Encode(removeList),
                            Result = "Success",
                            Type = "Delete",
                            Module = "SysGroupUser",
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

        public JsonResult GetMenuListByGroupId(string groupId)
        {
            List<SysGroupMenuCheck> menuChecklist = SysGroupMenuBizManager.GetMenuListByGroupId(groupId).ToList();

            SysGroupMenuActionQuery sysGroupMenuActionQuery = new SysGroupMenuActionQuery();
            sysGroupMenuActionQuery.GroupId = groupId;
            IList<SysGroupMenuActionCheck> allActionCheck = SysGroupMenuActionBizManager.GetCheckList(sysGroupMenuActionQuery);

            menuChecklist.ForEach(m =>
            {
                m.MenuActions = allActionCheck.Where(n => n.MenuId == m.MenuId).ToList();
            });

            return Json(menuChecklist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveGroupMenuChanges(string addData, string addAction)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                List<SysGroupMenu> addList = MiniUIHelper.Decode(addData, typeof(List<SysGroupMenu>)) as List<SysGroupMenu>;
                List<SysGroupMenuAction> addActionList = MiniUIHelper.Decode(addAction, typeof(List<SysGroupMenuAction>)) as List<SysGroupMenuAction>;

                result = SysGroupMenuBizManager.SaveGroupMenuChanges(addList, addActionList);

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
                            Module = "SysGroupMenu",
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

        public JsonResult GetOrgListByGroupId(string groupId)
        {
            IList<SysGroupOrgCheck> orgChecklist = SysGroupOrgBizManager.GetOrgListByGroupId(groupId);
            return Json(orgChecklist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveGroupOrgChanges(string addData)
        {
            ResultInfo result = new ResultInfo();
            try
            {
                List<SysGroupOrg> addList = MiniUIHelper.Decode(addData, typeof(List<SysGroupOrg>)) as List<SysGroupOrg>;
                result = SysGroupOrgBizManager.SaveGroupOrgChanges(addList);

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
                            Module = "SysGroupOrg",
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

        #endregion 用户组管理

        #region 权限认证
        [AllowAnonymous]
        public ActionResult AuthError()
        {
            return View();
        }
        #endregion 权限认证

        #region 会话过期
        [AllowAnonymous]
        public ActionResult SessionTimeout()
        {
            return View();
        }
        #endregion 会话过期
    }
}