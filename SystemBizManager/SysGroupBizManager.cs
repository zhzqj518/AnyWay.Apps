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
    public class SysGroupBizManager : BaseBizManager, ISysGroupBizManager
    {

        [Dependency]
        public ISysGroupRepository resp { get; set; }

        public TotalData GetTotalData(SysGroupQuery sysGroupQuery)
        {
            TotalData totalData = new TotalData();

            ModelQuery modelQuery = new ModelQuery();
            modelQuery.PageIndex = sysGroupQuery.PageIndex;
            modelQuery.PageSize = sysGroupQuery.PageSize;
            modelQuery.QueryParam = sysGroupQuery;
            totalData.total = resp.GetListCnt(modelQuery);
            totalData.data = resp.GetList(modelQuery);

            return totalData;
        }

        public ResultInfo SaveGroupChanges(List<SysGroup> addList, List<SysGroup> modifyList, List<SysGroup> removeList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            foreach (SysGroup entity in addList)
            {
                entity.GroupId = Guid.NewGuid().ToString("N");
                resp.Insert(entity);
            }

            foreach (SysGroup entity in modifyList)
            {
                SysGroup existModel = resp.GetById(entity);
                if (existModel == null)
                {
                    resultInfo.isSuccess = false;
                    resultInfo.ErrorMsgList.Add(string.Concat("用户组名称:", entity.GroupName, ",数据中不存在！"));
                    continue;
                }
                if (existModel.GroupUpdateTime != entity.GroupUpdateTime)
                {
                    if (existModel.GroupUpdateTime != null && entity.GroupUpdateTime != null)
                    {
                        if (existModel.GroupUpdateTime.Value.Subtract(entity.GroupUpdateTime.Value).TotalSeconds > 1)
                        {
                            resultInfo.isSuccess = false;
                            resultInfo.ErrorMsgList.Add(string.Concat("用户组名称:", entity.GroupName, ",数据在您操作之前已被修改！"));
                            continue;
                        }
                    }
                    else
                    {
                        resultInfo.isSuccess = false;
                        resultInfo.ErrorMsgList.Add(string.Concat("用户组名称:", entity.GroupName, ",数据在您操作之前已被修改！"));
                        continue;
                    }
                }

                entity.GroupUpdateTime = DateTime.Now;
                resp.Update(entity);
            }

            foreach (SysGroup entity in removeList)
            {
                resp.Delete(entity);
            }

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }
    }
}
