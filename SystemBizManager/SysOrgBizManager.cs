using ISystemBizManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.System;
using Unity.Attributes;
using Models.System.ViewModel;
using ISystemRepository;
using AnyWay.Apps.Core.Parameter;
using AnyWay.Apps.Persistence.Transaction;
using AnyWay.Apps.Core.Message;
using AnyWay.Apps.Core.BizManager;

namespace SystemBizManager
{
    public class SysOrgBizManager : BaseBizManager, ISysOrgBizManager
    {

        [Dependency]
        public ISysOrgRepository resp { get; set; }

        public IList<SysOrg> GetList(SysOrgQuery sysOrgQuery)
        {
            ModelQuery modelQuery = new ModelQuery();
            modelQuery.PageSize = 0;
            modelQuery.QueryParam = sysOrgQuery;

            return resp.GetList(modelQuery);
        }

        public ResultInfo SaveOrgChanges(List<SysOrg> addList, List<SysOrg> modifyList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            foreach (SysOrg entity in addList)
            {
                resp.Insert(entity);
            }

            foreach (SysOrg entity in modifyList)
            {
                SysOrg existModel = resp.GetById(entity);
                if (existModel == null)
                {
                    resultInfo.isSuccess = false;
                    resultInfo.ErrorMsgList.Add(string.Concat("组织机构名称:", entity.OrgName, ",数据中不存在！"));
                    continue;
                }
                if (existModel.OrgUpdateTime != entity.OrgUpdateTime)
                {
                    if (existModel.OrgUpdateTime != null && entity.OrgUpdateTime != null)
                    {
                        if (existModel.OrgUpdateTime.Value.Subtract(entity.OrgUpdateTime.Value).TotalSeconds > 1)
                        {
                            resultInfo.isSuccess = false;
                            resultInfo.ErrorMsgList.Add(string.Concat("组织机构名称:", entity.OrgName, ",数据在您操作之前已被修改！"));
                            continue;
                        }
                    }
                    else
                    {
                        resultInfo.isSuccess = false;
                        resultInfo.ErrorMsgList.Add(string.Concat("组织机构名称:", entity.OrgName, ",数据在您操作之前已被修改！"));
                        continue;
                    }
                }

                entity.OrgUpdateTime = DateTime.Now;
                resp.Update(entity);
            }

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }
    }
}
