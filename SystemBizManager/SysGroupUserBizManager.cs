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
    public class SysGroupUserBizManager : BaseBizManager, ISysGroupUserBizManager
    {

        [Dependency]
        public ISysGroupUserRepository resp { get; set; }

        public ResultInfo SaveAssignGroupUser(List<SysGroupUser> addList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            foreach (SysGroupUser entity in addList)
            {
                SysGroupUser item = resp.GetById(entity);
                if (item == null)
                {
                    resp.Insert(entity);
                }
            }

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }

        public ResultInfo SaveUnAssignGroupUser(List<SysGroupUser> removeList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            foreach (SysGroupUser entity in removeList)
            {
                resp.Delete(entity);
            }
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }

        public TotalData GetUnAssignSysUserTotalData(SysGroupUserQuery sysUserQuery)
        {
            TotalData totalData = new TotalData();

            totalData.total = resp.GetUnAssignUserListCnt(sysUserQuery);
            totalData.data = resp.GetUnAssignUserList(sysUserQuery);

            return totalData;
        }

        public TotalData GetAssignSysUserTotalData(SysGroupUserQuery sysUserQuery)
        {
            TotalData totalData = new TotalData();

            totalData.total = resp.GetAssignUserListCnt(sysUserQuery);
            totalData.data = resp.GetAssignUserList(sysUserQuery);

            return totalData;
        }
    }
}
