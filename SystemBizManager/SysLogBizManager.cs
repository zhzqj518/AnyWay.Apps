using AnyWay.Apps.Core.BizManager;
using AnyWay.Apps.Persistence.MyBatis;
using AnyWay.Apps.Persistence.Transaction;
using ISystemBizManager;
using ISystemRepository;
using Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace SystemBizManager
{
    public class SysLogBizManager : BaseBizManager, ISysLogBizManager
    {
        [Dependency]
        public ISysLogRepository resp { get; set; }

        public bool Insert(List<SysLog> List)
        {
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).BeginTransaction();
            foreach (SysLog entity in List)
            {
                resp.Insert(entity);
            }
            SqlTransactionFactory.GetInstance(SqlTransactionType.DB).CommitTransaction();
            return true;
        }
    }
}
