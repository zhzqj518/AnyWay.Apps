using AnyWay.Apps.Core.BizManager;
using AnyWay.Apps.Core.Message;
using AnyWay.Apps.Core.Parameter;
using AnyWay.Apps.Persistence.MyBatis;
using AnyWay.Apps.Persistence.Transaction;
using ISystemBizManager;
using ISystemRepository;
using Models.System;
using Models.System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace SystemBizManager
{
    public class SysMenuBizManager : BaseBizManager, ISysMenuBizManager
    {
        [Dependency]
        public ISysMenuRepository resp { get; set; }

        [Dependency]
        public ISysMenuActionRepository resp_action { get; set; }

        public SysMenu GetUserRoleMenu(SysMenuQuery menuQuery)
        {
            return resp.GetUserRoleMenu(menuQuery);
        }

        public IList<SysMenu> GetUserList(SysMenuQuery menuQuery)
        {
            if (menuQuery.UserType == "superadmin")
            {
                menuQuery.UserId = string.Empty;
            }
            return resp.GetListByUser(menuQuery);
        }


        public IList<SysMenu> GetList(SysMenuQuery menuQuery)
        {
            ModelQuery modelQuery = new ModelQuery();
            modelQuery.PageSize = 0;
            modelQuery.QueryParam = menuQuery;
            return resp.GetList(modelQuery);
        }

        public bool InsertList(List<SysMenu> entityList)
        {
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();
            foreach (SysMenu entity in entityList)
            {
                resp.Insert(entity);
            }
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();
            return true;
        }

        public bool UpdateList(List<SysMenu> entityList)
        {
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();
            foreach (SysMenu entity in entityList)
            {
                resp.Update(entity);
            }
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();
            return true;
        }

        public bool DeleteList(List<SysMenu> entityList)
        {
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();
            foreach (SysMenu entity in entityList)
            {
                resp.Delete(entity);
            }
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();
            return true;
        }

        public ResultInfo SaveMenuChanges(List<SysMenu> addList, List<SysMenu> modifyList, List<SysMenu> removeList, List<SysMenuAction> addActionList, List<SysMenuAction> modifyActionList, List<SysMenuAction> removeActionList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            foreach (SysMenu entity in addList)
            {
                resp.Insert(entity);
            }

            foreach (SysMenu entity in modifyList)
            {
                SysMenu existModel = resp.GetById(entity);
                if (existModel == null)
                {
                    resultInfo.isSuccess = false;
                    resultInfo.ErrorMsgList.Add(string.Concat("菜单名:", entity.MenuName, ",数据中不存在！"));
                    continue;
                }

                if (existModel.MenuUpdateTime != entity.MenuUpdateTime)
                {
                    if (existModel.MenuUpdateTime != null && entity.MenuUpdateTime != null)
                    {
                        if (existModel.MenuUpdateTime.Value.Subtract(entity.MenuUpdateTime.Value).TotalSeconds > 1)
                        {
                            resultInfo.isSuccess = false;
                            resultInfo.ErrorMsgList.Add(string.Concat("菜单名:", entity.MenuName, ",数据在您操作之前已被修改！"));
                            continue;
                        }
                    }
                    else
                    {
                        resultInfo.isSuccess = false;
                        resultInfo.ErrorMsgList.Add(string.Concat("菜单名:", entity.MenuName, ",数据在您操作之前已被修改！"));
                        continue;
                    }
                }

                entity.MenuUpdateTime = DateTime.Now;
                resp.Update(entity);
            }

            foreach (SysMenu entity in removeList)
            {
                resp.Delete(entity);
                resp_action.DeleteByMenuId(entity.MenuId);
            }

            foreach (SysMenuAction entity in addActionList)
            {
                resp_action.Insert(entity);
            }

            foreach (SysMenuAction entity in modifyActionList)
            {
                resp_action.Update(entity);
            }

            foreach (SysMenuAction entity in removeActionList)
            {
                resp_action.Delete(entity);
            }

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }

        public override void Dispose()
        {
            resp.Dispose();
        }
    }
}
