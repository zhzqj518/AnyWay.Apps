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
    public class SysGroupOrgBizManager : BaseBizManager, ISysGroupOrgBizManager
    {

        [Dependency]
        public ISysGroupOrgRepository resp { get; set; }

        public ResultInfo SaveGroupOrgChanges(List<SysGroupOrg> addList)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.isSuccess = true;
            resultInfo.ErrorMsgList = new List<string>();

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();

            SysGroupOrg item = addList.FirstOrDefault();

            resp.DeleteByGroupID(item);

            foreach (SysGroupOrg entity in addList)
            {
                if (!string.IsNullOrEmpty(entity.OrgId))
                {
                    resp.Insert(entity);
                }
            }

            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();

            return resultInfo;
        }

        public IList<SysGroupOrgCheck> GetOrgListByGroupId(string groupId)
        {
            return resp.GetOrgListByGroupId(groupId);
        }
    }
}
