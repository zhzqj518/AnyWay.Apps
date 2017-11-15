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
    public class SysMenuSpclBizManager : BaseBizManager, ISysMenuSpclBizManager
    {
        [Dependency]
        public ISysMenuSpclRepository resp { get; set; }

        public IList<SysMenuSpcl> GetList(SysMenuSpclQuery menuSpclQuery)
        {
            ModelQuery modelQuery = new ModelQuery();
            modelQuery.PageSize = 0;
            modelQuery.QueryParam = menuSpclQuery;
            return resp.GetList(modelQuery);
        }

        public ResultInfo SaveMenuSpclChanges(List<SysMenuSpcl> addList, List<SysMenuSpcl> modifyList, List<SysMenuSpcl> removeList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            foreach (SysMenuSpcl entity in addList)
            {
                resp.Insert(entity);
            }

            foreach (SysMenuSpcl entity in modifyList)
            {
                SysMenuSpcl existModel = resp.GetById(entity);
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

            foreach (SysMenuSpcl entity in removeList)
            {
                resp.Delete(entity);
            }
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }
    }
}
